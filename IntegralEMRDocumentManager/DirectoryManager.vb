Public Class DirectoryManager
    Protected mDirectoryName As String
    Protected mDirectoryInfo As IO.DirectoryInfo
    Protected mFileInfo As IO.FileInfo()
    Protected mFilesList As Collection
    Public Sub New(ByVal directory As String)
        mDirectoryName = directory
        mDirectoryInfo = New IO.DirectoryInfo(mDirectoryName)
        mFileInfo = mDirectoryInfo.GetFiles("*.doc")
    End Sub

    Protected Sub InitializeFilesList()
        If IsNothing(mFilesList) Then
            mFilesList = New Collection
            For Each fi As IO.FileInfo In mFileInfo
                mFilesList.Add(New IEMRFileInfo(fi), fi.Name)
            Next
        End If
    End Sub


    Public Sub PopulateGrid(ByRef dg As DataGridView)
        InitializeFilesList()
        dg.DataSource = mFilesList
        dg.Columns("FullName").Visible = False
        dg.Columns("Filename").Width = 140
        dg.Columns("Message").Width = 240
        dg.Refresh()
    End Sub

    Public Function GenerateFilesList(ByVal filter As String)
        Dim retval As String = ""
        For Each fi As IEMRFileInfo In mFilesList
            retval = retval + "," + fi.Filename
        Next
        Return retval
    End Function

    Public Sub UpdateStatusFromBrowser(ByRef brw As WebBrowser)

        Dim tbody As HtmlElement
        tbody = brw.Document.GetElementById("libreStatus")
        For Each tr As HtmlElement In tbody.Children
            Dim children As HtmlElementCollection = tr.Children
            Dim fi As IEMRFileInfo
            Dim serverFN = children.Item(0).InnerText() + ".DOC"
            fi = mFilesList.Item(serverFN)
            fi.Status = "Found"
            If (children.Count >= 4) Then
                Dim status = (children.Item(3).InnerText() = "Successful")
                If status Then
                    If children.Item(2).InnerText() = "Import" Then
                        fi.Status = "Complete"
                        fi.Message = ""
                    End If
                Else
                    fi.Message = children.Item(1).InnerText()
                    fi.Status = children.Item(2).InnerText()
                End If
            End If
        Next
    End Sub

    Public Function UnsuccessfulFiles()
        Dim retval As New Collection
        For Each fi As IEMRFileInfo In mFilesList
            If (fi.Status = "Complete") Then
            Else
                retval.Add(fi)
            End If
        Next
        Return retval
    End Function
End Class
