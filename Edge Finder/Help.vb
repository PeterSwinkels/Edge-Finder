'This class' imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Drawing

'This class contains this program's help window.
Public Class HelpWindow
   'This procedure initializes this window.
   Public Sub New()
      InitializeComponent()

      Me.Text = $"{My.Application.Info.Title} - Help"

      With My.Computer.Screen.WorkingArea
         Me.Size = New Size(CInt(.Width / 1.5), CInt(.Height / 1.5))
      End With

      With HelpTextBox
         .Text = My.Resources.Edge_Finder_Help
         .Select(0, 0)
      End With
   End Sub
End Class