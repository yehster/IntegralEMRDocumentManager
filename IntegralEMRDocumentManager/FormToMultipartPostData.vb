
' This is a helper class to build postData header an body with the values from 
' the given HTML form. The purpose of this class is to attach files to 
' the <INPUT TYPE="FILE"> fields.
' 
' License: Code Project Open License (CPOL)
' (c) Kirill Hryapin, 2008
' (c) Steven Cheng (Microsoft), 2005 (?)
' (c) Kevin Yeh, 2012 - adapted c# to vb

Public Class FormToMultipartPostData



    Private values As New List(Of ValuePair)

    Private files As New List(Of ValuePair)
    Private overloadedFiles As New Dictionary(Of String, String)

    Protected form As HtmlElement
    Protected webbrowser As WebBrowser

    Public Sub New(ByRef b As WebBrowser, ByRef f As HtmlElement)
        form = f
        webbrowser = b
        GetValuesFromForm(f)
    End Sub


    Public Sub Submit()
        Dim url As Uri = New Uri(webbrowser.Url, form.GetAttribute("action"))
        Dim req As RequestParameters = GetEncodedPostData()
        Dim target As String = form.GetAttribute("target")
        target = "_top"
        webbrowser.Navigate(url, target, req.data, req.headers)
    End Sub

    Private Sub GetValuesFromForm(ByVal form As HtmlElement)
        For Each child As HtmlElement In form.All
            If child.TagName = "INPUT" Then
                Dim elemType As String = child.GetAttribute("type").ToUpper()
                If elemType = "FILE" Then
                    AddFile(child.Name, child.GetAttribute("value"))
                ElseIf (elemType = "CHECKBOX") Or elemType = "RADIO" Then
                    If (child.GetAttribute("checked") = "True") Then
                        AddValue(child.Name, child.GetAttribute("value"))
                    End If
                ElseIf (elemType = "BUTTON") Or (elemType = "IMAGE") Or (elemType = "Reset") Then
                Else
                    AddValue(child.Name, child.GetAttribute("value"))
                End If
            ElseIf (child.TagName = "TEXTAREA") Or (child.TagName = "SELECT") Then
                AddValue(child.Name, child.GetAttribute("value"))
            End If
        Next

    End Sub


    Private Sub AddValue(ByVal name As String, ByVal value As String)
        If name = "" Then
            Return
        End If
        values.Add(New ValuePair(name, value))
    End Sub

    Private Sub AddFile(ByVal name As String, ByVal value As String)
        If name = "" Then
            Return
        End If
        files.Add(New ValuePair(name, value))
    End Sub

    ' Set file field value [the reason why this class exist]
    Public Sub SetFile(ByVal fieldName As String, ByVal filePath As String)
        overloadedFiles.Add(fieldName, filePath)
    End Sub

    ' One may need it to know whether there's specific file input
    ' For example, to perform some actions (think format conversion) before uploading
    Public Function HasFileField(ByVal fieldName As String) As Boolean
        For Each v As ValuePair In files
            If v.name = fieldName Then
                Return True
            End If
        Next
        Return False
    End Function


    Public Function GetEncodedPostData() As RequestParameters
        Dim boundary As String = "----------------------------" + DateTime.Now.Ticks.ToString("x")

        Dim memStream As New System.IO.MemoryStream()
        Dim boundarybytes() As Byte = System.Text.Encoding.ASCII.GetBytes(vbCrLf + "--" + boundary + vbCrLf)

        Dim formdataTemplate As String = vbCrLf + "--" + boundary + vbCrLf + "Content-Disposition: form-data; name=""{0}"";" + vbCrLf + vbCrLf + "{1}"
        For Each v As ValuePair In values

            Dim formitem As String = String.Format(formdataTemplate, v.name, v.value)
            Dim formitembytes() As Byte = System.Text.Encoding.UTF8.GetBytes(formitem)
            memStream.Write(formitembytes, 0, formitembytes.Length)
        Next
        memStream.Write(boundarybytes, 0, boundarybytes.Length)

        Dim headerTemplate As String = "Content-Disposition: form-data; name=""{0}""; filename=""{1}""" + vbCrLf + "Content-Type: application/octet-stream" + vbCrLf + vbCrLf

        For Each v As ValuePair In files

            Dim filePath As String

            If (overloadedFiles.ContainsKey(v.name)) Then
                filePath = overloadedFiles(v.name)
            Else

                If (v.value.Length = 0) Then Continue For
                filePath = v.value
            End If
            Try

                Dim fileStream As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)

                Dim header As String = String.Format(headerTemplate, v.name, filePath)
                Dim headerbytes() As Byte = System.Text.Encoding.UTF8.GetBytes(header)
                memStream.Write(headerbytes, 0, headerbytes.Length)

                Dim buffer(1024) As Byte
                Dim bytesRead As Integer = 0
                bytesRead = fileStream.Read(buffer, 0, buffer.Length)
                While (bytesRead <> 0)
                    memStream.Write(buffer, 0, bytesRead)
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length)
                End While

                memStream.Write(boundarybytes, 0, boundarybytes.Length)
                fileStream.Close()

            Catch x As Exception
                MessageBox.Show(x.Message, "Cannot upload the file", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        Next

        Dim result As New RequestParameters()

        memStream.Position = 0
        ReDim result.data(memStream.Length)
        memStream.Read(result.data, 0, result.data.Length)
        memStream.Close()

        result.headers = "Content-Type: multipart/form-data; boundary=" + boundary + vbCrLf +
                         "Content-Length: " + CStr(result.data.Length) + vbCrLf +
                         vbCrLf

        Return result

    End Function
End Class
