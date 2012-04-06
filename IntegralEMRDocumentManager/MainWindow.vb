Public Class MainWindow
    Dim mDirMgr As New DirectoryManager("E:\MedVocab\MSA01092012002")
    Dim mOEMRServer As String = "http://ubuntu/openemr"
    Dim mUserID As String = "admin"

    Private Function OEMRPassword() As String
        Return "pass"
    End Function

    Private Sub MainWindow_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mDirMgr.PopulateGrid(dgFileStatuses)
        wbOpenEMRSession.Navigate(mOEMRServer)
        Login(wbOpenEMRSession, "admin", "pass")

    End Sub



    Protected Function Login(ByRef Browser As WebBrowser, ByVal userID As String, ByVal pwd As String)
        Browser.Navigate(mOEMRServer)
        WaitForNavigate(Browser)
        Dim frameDoc = Browser.Document.Window.Frames.Item("Login").Document
        Dim inputs = frameDoc.GetElementsByTagName("input")
        Dim user = inputs.GetElementsByName("authUser").Item(0)
        Dim pass = inputs.GetElementsByName("clearPass").Item(0)
        user.SetAttribute("value", mUserID)
        pass.SetAttribute("value", OEMRPassword())
        inputs.Item(5).InvokeMember("click")

        Return True
    End Function

    Protected Sub WaitForNavigate(ByVal Browser)
        Do
            Application.DoEvents()
        Loop Until (Browser.ReadyState = WebBrowserReadyState.Complete)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        WaitForNavigate(wbOpenEMRSession)
        Dim testPost = "files=" + mDirMgr.GenerateFilesList("") + "&stuff=more stuff"
        Dim postData = System.Text.Encoding.UTF8.GetBytes(testPost)
        WaitForNavigate(wbOpenEMRSession)
        wbOpenEMRSession.Navigate(mOEMRServer + "/library/doctrine/batch/libreStatus.php", "_top", postData, "Content-Type: application/x-www-form-urlencoded")
    End Sub

    Private Sub HandleLogin(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs)
        Dim Browser As WebBrowser = sender
        RemoveHandler Browser.Navigated, AddressOf HandleLogin

    End Sub
End Class
