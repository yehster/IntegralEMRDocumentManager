Public Class IEMRFileInfo
    Protected mFI As IO.FileInfo
    Protected mStatus As String
    Protected mMessage As String

    Public Sub New(ByVal fi As IO.FileInfo)
        mFI = fi
        mStatus = "Unknown"
    End Sub

    Public ReadOnly Property Filename As String
        Get
            Return mFI.Name
        End Get
    End Property
    Public ReadOnly Property FullName As String
        Get
            Return mFI.FullName
        End Get
    End Property
    Public Property Status As String
        Get
            Return mStatus
        End Get
        Set(ByVal value As String)
            mStatus = value
        End Set
    End Property

    Public Property Message As String
        Get
            Return mMessage
        End Get
        Set(ByVal value As String)
            mMessage = value
        End Set
    End Property
End Class
