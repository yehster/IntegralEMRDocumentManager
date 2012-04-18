<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgFileStatuses = New System.Windows.Forms.DataGridView()
        Me.wbOpenEMRSession = New System.Windows.Forms.WebBrowser()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnBatch = New System.Windows.Forms.Button()
        CType(Me.dgFileStatuses, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgFileStatuses
        '
        Me.dgFileStatuses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgFileStatuses.Location = New System.Drawing.Point(4, 2)
        Me.dgFileStatuses.Name = "dgFileStatuses"
        Me.dgFileStatuses.Size = New System.Drawing.Size(565, 342)
        Me.dgFileStatuses.TabIndex = 0
        '
        'wbOpenEMRSession
        '
        Me.wbOpenEMRSession.Location = New System.Drawing.Point(575, 12)
        Me.wbOpenEMRSession.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbOpenEMRSession.Name = "wbOpenEMRSession"
        Me.wbOpenEMRSession.Size = New System.Drawing.Size(641, 514)
        Me.wbOpenEMRSession.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(22, 375)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 30)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(29, 450)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(113, 38)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Send File"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnBatch
        '
        Me.btnBatch.Location = New System.Drawing.Point(29, 540)
        Me.btnBatch.Name = "btnBatch"
        Me.btnBatch.Size = New System.Drawing.Size(113, 38)
        Me.btnBatch.TabIndex = 4
        Me.btnBatch.Text = "Send Batch"
        Me.btnBatch.UseVisualStyleBackColor = True
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(1237, 719)
        Me.Controls.Add(Me.btnBatch)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.wbOpenEMRSession)
        Me.Controls.Add(Me.dgFileStatuses)
        Me.Name = "MainWindow"
        Me.Text = "Integral EMR Document Manager"
        CType(Me.dgFileStatuses, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgFileStatuses As System.Windows.Forms.DataGridView
    Friend WithEvents wbOpenEMRSession As System.Windows.Forms.WebBrowser
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnBatch As System.Windows.Forms.Button

End Class
