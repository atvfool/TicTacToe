<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRecentGames
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
        Me.lstRecentGames = New System.Windows.Forms.ListBox()
        Me.btnReplayGame = New System.Windows.Forms.Button()
        Me.btnViewBoard = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstRecentGames
        '
        Me.lstRecentGames.FormattingEnabled = True
        Me.lstRecentGames.ItemHeight = 20
        Me.lstRecentGames.Location = New System.Drawing.Point(13, 13)
        Me.lstRecentGames.Name = "lstRecentGames"
        Me.lstRecentGames.Size = New System.Drawing.Size(606, 224)
        Me.lstRecentGames.TabIndex = 0
        '
        'btnReplayGame
        '
        Me.btnReplayGame.Location = New System.Drawing.Point(13, 353)
        Me.btnReplayGame.Name = "btnReplayGame"
        Me.btnReplayGame.Size = New System.Drawing.Size(148, 49)
        Me.btnReplayGame.TabIndex = 1
        Me.btnReplayGame.Text = "Replay Game"
        Me.btnReplayGame.UseVisualStyleBackColor = True
        '
        'btnViewBoard
        '
        Me.btnViewBoard.Location = New System.Drawing.Point(243, 353)
        Me.btnViewBoard.Name = "btnViewBoard"
        Me.btnViewBoard.Size = New System.Drawing.Size(148, 49)
        Me.btnViewBoard.TabIndex = 2
        Me.btnViewBoard.Text = "View Board"
        Me.btnViewBoard.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(471, 353)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(148, 49)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'FRecentGames
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 414)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnViewBoard)
        Me.Controls.Add(Me.btnReplayGame)
        Me.Controls.Add(Me.lstRecentGames)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FRecentGames"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Recent Games"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstRecentGames As System.Windows.Forms.ListBox
    Friend WithEvents btnReplayGame As System.Windows.Forms.Button
    Friend WithEvents btnViewBoard As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
