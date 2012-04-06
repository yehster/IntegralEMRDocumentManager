Public Class DirectoryManager
    Protected mDirectoryName As String
    Protected mDirectoryInfo As IO.DirectoryInfo
    Protected mFileInfo As IO.FileInfo()
    Protected mFilesList As List(Of IEMRFileInfo)

    Public Sub New(ByVal directory As String)
        mDirectoryName = directory
        mDirectoryInfo = New IO.DirectoryInfo(mDirectoryName)
        mFileInfo = mDirectoryInfo.GetFiles("*.doc")
    End Sub

    Protected Sub InitializeFilesList()
        mFilesList = New List(Of IEMRFileInfo)
        For Each fi As IO.FileInfo In mFileInfo
            mFilesList.Add(New IEMRFileInfo(fi))
        Next
    End Sub


    Public Sub PopulateGrid(ByRef dg As DataGridView)
        InitializeFilesList()
        dg.DataSource = mFilesList
    End Sub

    Public Function GenerateFilesList(ByVal filter As String)
        Dim retval As String = ""
        For Each fi As IEMRFileInfo In mFilesList
            retval = retval + "," + fi.Filename
        Next
        Return retval
    End Function
End Class
