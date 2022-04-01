'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System
Imports System.Drawing
Imports System.Environment
Imports System.Linq
Imports System.Math
Imports System.Text

'This module contains this program's color processing procedures.
Public Module ColorProcessorModule
   'This enumeration lists the color matching modes.
   Public Enum ColorMatchModesE As Integer
      BrightnessMode   'Luminance matching mode.
      HueMode          'Hue matching mode.
      RGBMode          'RGB matching mode.
   End Enum

   Public ReadOnly MAXIMUM_COLOR_TOLERANCES() As Integer = {255, 240, 255}                         'Defines the maximum tolerance for each color match mode.
   Private ReadOnly COLOR_MATCH_MODES() As String = {"Brightness", "Hue", "Reg, Green, and Blue"}   'Defines the color match mode descriptions.

   'This procedure checks whether the two specified colors match within the tolerance specified.
   Public Function ColorsMatch(color1 As Color, color2 As Color, colorMatchMode As ColorMatchModesE, tolerance As Integer) As Boolean
      Select Case colorMatchMode
         Case ColorMatchModesE.HueMode
            Return (Abs(color2.GetHue - color1.GetHue) <= tolerance)
         Case ColorMatchModesE.BrightnessMode
            Return (Abs((color2.GetBrightness * Byte.MaxValue) - (color1.GetBrightness * Byte.MaxValue)) <= tolerance)
         Case ColorMatchModesE.RGBMode
            Return ({Abs(CInt(color2.R) - CInt(color1.R)), Abs(CInt(color2.G) - CInt(color1.G)), Abs(CInt(color2.B) - CInt(color1.B))}.Average() <= tolerance)
      End Select

      Return False
   End Function

   'This procedure manages the current color match mode.
   Public Function ColorMatchMode(Optional newColorMatchMode As ColorMatchModesE? = Nothing) As ColorMatchModesE?
      Static currentColorMatchMode As ColorMatchModesE = ColorMatchModesE.RGBMode

      If newColorMatchMode IsNot Nothing Then
         If Array.IndexOf([Enum].GetValues(GetType(ColorMatchModesE)), newColorMatchMode.Value) >= 0 Then
            currentColorMatchMode = newColorMatchMode.Value
            ColorTolerance(newColorTolerance:=0)
         Else
            Return Nothing
         End If
      End If

      Return currentColorMatchMode
   End Function

   'This procedure returns a list of color match mode descriptions.
   Public Function ColorMatchModeDescriptions() As String
      Dim descriptions As New StringBuilder

      For colorMatchModes As Integer = COLOR_MATCH_MODES.GetLowerBound(0) To COLOR_MATCH_MODES.GetUpperBound(0)
         descriptions.Append($"{colorMatchModes} = {COLOR_MATCH_MODES(colorMatchModes)}")
         If colorMatchModes < COLOR_MATCH_MODES.GetUpperBound(0) Then descriptions.Append(NewLine)
      Next colorMatchModes

      Return descriptions.ToString()
   End Function

   'This procedure manages the color difference tolerance.
   Public Function ColorTolerance(Optional newColorTolerance As Integer? = Nothing) As Integer?
      Static currentColorTolerance As Integer = 0

      If newColorTolerance IsNot Nothing Then
         If newColorTolerance >= 0 AndAlso newColorTolerance <= MAXIMUM_COLOR_TOLERANCES(ColorMatchMode().Value) Then
            currentColorTolerance = newColorTolerance.Value
         Else
            currentColorTolerance = 0
            Return Nothing
         End If
      End If

      Return currentColorTolerance
   End Function
End Module
