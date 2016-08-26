<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FTicTacToe
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
		Me.btn1 = New System.Windows.Forms.Button()
		Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
		Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.NewGameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ViewRecentGamesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.btn2 = New System.Windows.Forms.Button()
		Me.btn3 = New System.Windows.Forms.Button()
		Me.btn4 = New System.Windows.Forms.Button()
		Me.btn5 = New System.Windows.Forms.Button()
		Me.btn6 = New System.Windows.Forms.Button()
		Me.btn7 = New System.Windows.Forms.Button()
		Me.btn8 = New System.Windows.Forms.Button()
		Me.btn9 = New System.Windows.Forms.Button()
		Me.MenuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'btn1
		'
		Me.btn1.Location = New System.Drawing.Point(12, 27)
		Me.btn1.Name = "btn1"
		Me.btn1.Size = New System.Drawing.Size(74, 45)
		Me.btn1.TabIndex = 0
		Me.btn1.UseVisualStyleBackColor = True
		'
		'MenuStrip1
		'
		Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
		Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.MenuStrip1.Name = "MenuStrip1"
		Me.MenuStrip1.Size = New System.Drawing.Size(257, 24)
		Me.MenuStrip1.TabIndex = 1
		Me.MenuStrip1.Text = "MenuStrip1"
		'
		'FileToolStripMenuItem
		'
		Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewGameToolStripMenuItem, Me.ViewRecentGamesToolStripMenuItem, Me.ExitToolStripMenuItem})
		Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
		Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.FileToolStripMenuItem.Text = "File"
		'
		'NewGameToolStripMenuItem
		'
		Me.NewGameToolStripMenuItem.Name = "NewGameToolStripMenuItem"
		Me.NewGameToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.NewGameToolStripMenuItem.Text = "New Game"
		'
		'ViewRecentGamesToolStripMenuItem
		'
		Me.ViewRecentGamesToolStripMenuItem.Name = "ViewRecentGamesToolStripMenuItem"
		Me.ViewRecentGamesToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.ViewRecentGamesToolStripMenuItem.Text = "View Recent Games"
		'
		'ExitToolStripMenuItem
		'
		Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
		Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.ExitToolStripMenuItem.Text = "Exit"
		'
		'btn2
		'
		Me.btn2.Location = New System.Drawing.Point(92, 27)
		Me.btn2.Name = "btn2"
		Me.btn2.Size = New System.Drawing.Size(74, 45)
		Me.btn2.TabIndex = 2
		Me.btn2.UseVisualStyleBackColor = True
		'
		'btn3
		'
		Me.btn3.Location = New System.Drawing.Point(172, 27)
		Me.btn3.Name = "btn3"
		Me.btn3.Size = New System.Drawing.Size(74, 45)
		Me.btn3.TabIndex = 3
		Me.btn3.UseVisualStyleBackColor = True
		'
		'btn4
		'
		Me.btn4.Location = New System.Drawing.Point(12, 78)
		Me.btn4.Name = "btn4"
		Me.btn4.Size = New System.Drawing.Size(74, 45)
		Me.btn4.TabIndex = 4
		Me.btn4.UseVisualStyleBackColor = True
		'
		'btn5
		'
		Me.btn5.Location = New System.Drawing.Point(92, 78)
		Me.btn5.Name = "btn5"
		Me.btn5.Size = New System.Drawing.Size(74, 45)
		Me.btn5.TabIndex = 5
		Me.btn5.UseVisualStyleBackColor = True
		'
		'btn6
		'
		Me.btn6.Location = New System.Drawing.Point(172, 78)
		Me.btn6.Name = "btn6"
		Me.btn6.Size = New System.Drawing.Size(74, 45)
		Me.btn6.TabIndex = 6
		Me.btn6.UseVisualStyleBackColor = True
		'
		'btn7
		'
		Me.btn7.Location = New System.Drawing.Point(12, 129)
		Me.btn7.Name = "btn7"
		Me.btn7.Size = New System.Drawing.Size(74, 45)
		Me.btn7.TabIndex = 7
		Me.btn7.UseVisualStyleBackColor = True
		'
		'btn8
		'
		Me.btn8.Location = New System.Drawing.Point(92, 129)
		Me.btn8.Name = "btn8"
		Me.btn8.Size = New System.Drawing.Size(74, 45)
		Me.btn8.TabIndex = 8
		Me.btn8.UseVisualStyleBackColor = True
		'
		'btn9
		'
		Me.btn9.Location = New System.Drawing.Point(172, 129)
		Me.btn9.Name = "btn9"
		Me.btn9.Size = New System.Drawing.Size(74, 45)
		Me.btn9.TabIndex = 9
		Me.btn9.UseVisualStyleBackColor = True
		'
		'FTicTacToe
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(257, 188)
		Me.Controls.Add(Me.btn9)
		Me.Controls.Add(Me.btn8)
		Me.Controls.Add(Me.btn7)
		Me.Controls.Add(Me.btn6)
		Me.Controls.Add(Me.btn5)
		Me.Controls.Add(Me.btn4)
		Me.Controls.Add(Me.btn3)
		Me.Controls.Add(Me.btn2)
		Me.Controls.Add(Me.btn1)
		Me.Controls.Add(Me.MenuStrip1)
		Me.MainMenuStrip = Me.MenuStrip1
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "FTicTacToe"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "FTicTacToe"
		Me.MenuStrip1.ResumeLayout(False)
		Me.MenuStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

End Sub
    Friend WithEvents btn1 As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents btn2 As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.Button
    Friend WithEvents btn4 As System.Windows.Forms.Button
    Friend WithEvents btn5 As System.Windows.Forms.Button
    Friend WithEvents btn6 As System.Windows.Forms.Button
    Friend WithEvents btn7 As System.Windows.Forms.Button
    Friend WithEvents btn8 As System.Windows.Forms.Button
    Friend WithEvents btn9 As System.Windows.Forms.Button
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewGameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewRecentGamesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
