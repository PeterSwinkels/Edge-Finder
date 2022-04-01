<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HelpWindow
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
        Me.HelpTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'HelpTextBox
        '
        Me.HelpTextBox.BackColor = System.Drawing.SystemColors.Window
        Me.HelpTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpTextBox.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpTextBox.HideSelection = False
        Me.HelpTextBox.Location = New System.Drawing.Point(0, 0)
        Me.HelpTextBox.Multiline = True
        Me.HelpTextBox.Name = "HelpTextBox"
        Me.HelpTextBox.ReadOnly = True
        Me.HelpTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.HelpTextBox.Size = New System.Drawing.Size(800, 450)
        Me.HelpTextBox.TabIndex = 0
        Me.HelpTextBox.WordWrap = False
        '
        'HelpWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.HelpTextBox)
        Me.KeyPreview = True
        Me.Name = "HelpWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents HelpTextBox As System.Windows.Forms.TextBox
End Class
