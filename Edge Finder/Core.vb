'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports Newtonsoft.Json
Imports System
Imports System.Drawing
Imports System.Environment
Imports System.IO
Imports System.Windows.Forms

'This module contains this program's core procedures.
Public Module CoreModule

   'This class contains this program's user settings.
   Public Class UserSettingsClass
      Public Property ColorMatchMode As ColorMatchModesE = Nothing       'Defines the color match mode.
      Public Property ColorTolerance As Integer = 0                      'Defines the color difference tolerance.
      Public Property CompareRelativeEdgeCoordinates As Boolean = True   'Defines whether or not relative edge comparison should be used.
      Public Property DisplayEdgeSourceImage As Boolean = True           'Defines whether or not the edge source image is displayed.
      Public Property DoNotCompareEdgeSize As Boolean = True             'Defines whether or not edge sizes should be ignored while comparing.
      Public Property EdgeHighlightColor As Color = Color.Red            'Defines the highlighting color for the selected edge.
      Public Property EdgePointDisplaySize As Integer = 2                'Defines display size for the points that make up an edge.
      Public Property EdgesDisplayColor As Color = Color.LightGreen      'Defines the color for displaying edges.
      Public Property MinimumEdgeLength As Integer = 2                   'Defines the minimum length an edge should have.
      Public Property ViewAllEdges As Boolean = True                     'Defines whether all or just the selected edge is displayed.
      Public Property WindowPosition As Point? = Nothing                 'Defines the main interface window's position.
      Public Property WindowSize As Size? = Nothing                      'Defines the main interface window's size.
   End Class

   Private ReadOnly USER_SETTINGS_FILE As String = $"{My.Application.Info.Title}.Settings"       'Defines the settings file's name.

   'This procedure displays any errors that occur.
   Public Sub DisplayError(exceptionObj As Exception)
      Try
         MessageBox.Show(exceptionObj.Message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      Catch
         [Exit](0)
      End Try
   End Sub

   'This procedure returns information about this program.
   Public Function ProgramInformation() As String
      With My.Application.Info
         Return $"{ .Title} v{ .Version} - by: { .CompanyName}"
      End With
   End Function

   'This procedure saves the program's settings.
   Public Sub SaveUserSettings(userSettings As UserSettingsClass)
      Try
         File.WriteAllText(USER_SETTINGS_FILE, JsonConvert.SerializeObject(userSettings, formatting:=Formatting.Indented))
      Catch ExceptionObj As Exception
         DisplayError(ExceptionObj)
      End Try
   End Sub

   'This procedure loads this program's settings upon request and returns these.
   Public Function UserSettings(Optional refresh As Boolean = False) As UserSettingsClass
      Try
         Static currentUserSettings As New UserSettingsClass

         If refresh AndAlso File.Exists(USER_SETTINGS_FILE) Then
            currentUserSettings = JsonConvert.DeserializeObject(Of UserSettingsClass)(File.ReadAllText(USER_SETTINGS_FILE))
         End If

         Return currentUserSettings
      Catch ExceptionObj As Exception
         DisplayError(ExceptionObj)
      End Try

      Return Nothing
   End Function
End Module
