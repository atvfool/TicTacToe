<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FMain
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
        Me.btnCounterClockwise = New System.Windows.Forms.Button()
        Me.btnClockwise = New System.Windows.Forms.Button()
        Me.btnIn = New System.Windows.Forms.Button()
        Me.btnOut = New System.Windows.Forms.Button()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCounterClockwise
        '
        Me.btnCounterClockwise.Location = New System.Drawing.Point(32, 58)
        Me.btnCounterClockwise.Name = "btnCounterClockwise"
        Me.btnCounterClockwise.Size = New System.Drawing.Size(69, 40)
        Me.btnCounterClockwise.TabIndex = 11
        Me.btnCounterClockwise.Text = "Counter Clockwise"
        Me.btnCounterClockwise.UseVisualStyleBackColor = True
        '
        'btnClockwise
        '
        Me.btnClockwise.Location = New System.Drawing.Point(181, 58)
        Me.btnClockwise.Name = "btnClockwise"
        Me.btnClockwise.Size = New System.Drawing.Size(69, 40)
        Me.btnClockwise.TabIndex = 10
        Me.btnClockwise.Text = "Clockwise"
        Me.btnClockwise.UseVisualStyleBackColor = True
        '
        'btnIn
        '
        Me.btnIn.Location = New System.Drawing.Point(107, 104)
        Me.btnIn.Name = "btnIn"
        Me.btnIn.Size = New System.Drawing.Size(69, 40)
        Me.btnIn.TabIndex = 9
        Me.btnIn.Text = "In"
        Me.btnIn.UseVisualStyleBackColor = True
        '
        'btnOut
        '
        Me.btnOut.Location = New System.Drawing.Point(107, 12)
        Me.btnOut.Name = "btnOut"
        Me.btnOut.Size = New System.Drawing.Size(69, 40)
        Me.btnOut.TabIndex = 8
        Me.btnOut.Text = "Out"
        Me.btnOut.UseVisualStyleBackColor = True
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(201, 109)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(69, 40)
        Me.btnTest.TabIndex = 12
        Me.btnTest.Text = "Test 1"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'FMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(282, 161)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.btnCounterClockwise)
        Me.Controls.Add(Me.btnClockwise)
        Me.Controls.Add(Me.btnIn)
        Me.Controls.Add(Me.btnOut)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tic Tac Toe"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCounterClockwise As System.Windows.Forms.Button
    Friend WithEvents btnClockwise As System.Windows.Forms.Button
    Friend WithEvents btnIn As System.Windows.Forms.Button
    Friend WithEvents btnOut As System.Windows.Forms.Button
    Friend WithEvents btnTest As System.Windows.Forms.Button

End Class
