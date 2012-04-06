Public Class IEMRFileInfo
    Protected mFI As IO.FileInfo
    Protected mStatus As String
    Public Sub New(ByVal fi As IO.FileInfo)
        mFI = fi
        mStatus = "Unknown"
    End Sub

    Public ReadOnly Property Filename As String
        Get
            Return mFI.Name
        End Get
    End Property

    Public ReadOnly Property Status As String
        Get
            Return mStatus
        End Get
    End Property
End Class
