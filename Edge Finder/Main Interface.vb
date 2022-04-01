'This class' imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

'This class contains this program's main interface window.
Public Class MainInterfaceWindow
   'This enumaration lists the edge selection actions.
   Private Enum EdgeSelectionActionsE As Integer
      None           'Indicates that no action is performed.
      Compare        'Compares two edges.
      GoToFirst      'Selects the first edge.
      GoToLast       'Selects the last edge.
      NextEdge       'Selects the next edge.
      PreviousEdge   'Selects the previous edge.
      SelectEdge     'Selects the edge with the specified index.
      SetCompared1   'Selects the current edge for comparisson.
      SetCompared2   'Selects the current edge for comparisson.
   End Enum

   'This structure defines the edge difference information.
   Private Structure EdgeDifferenceStr
      Public Difference As Double     'Defines the edge difference.
      Public Edge As List(Of Point)   'Defines the edge.
   End Structure

   'This structure defines the edge selecion information.
   Private Structure EdgeSelectionStr
      Public Compare1 As Integer?     'Defines which edge is to be compared with another edge.
      Public Compare2 As Integer?     'Defines which edge is the "other" to be compared.
      Public Current As Integer       'Defines the current edge.
      Public Difference As Double     'Defines the difference between two edges.
   End Structure

   Private ReadOnly IMAGE_EXTENSIONS As New List(Of String)({".bmp", ".emf", ".exif", ".gif", ".ico", ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".wmf"})   'Defines the extensions of the supported image file types.

   'This procedure initializes this window.
   Public Sub New()
      InitializeComponent()

      My.Application.ChangeCulture("en-US")
      My.Computer.FileSystem.CurrentDirectory = My.Application.Info.DirectoryPath

      Me.Text = ProgramInformation()

      With My.Computer.Screen.WorkingArea
         Me.Size = New Size(CInt(.Width / 1.1), CInt(.Height / 1.1))
      End With

      With ImageBox
         .Size = ImagePanel.Size
         .Image = EdgeSourceImage(, newSourceImage:=New Bitmap(.ClientSize.Width, .ClientSize.Height))
         Graphics.FromImage(.Image).FillRectangle(Brushes.White, .ClientRectangle)
      End With

      With UserSettings(Refresh:=True)
         AllEdgesMenu.Checked = .ViewAllEdges
         EdgesDisplayColorDialog.Color = .EdgesDisplayColor
         EdgeHighlightColorDialog.Color = .EdgeHighlightColor
         EdgeSourceImageMenu.Checked = .DisplayEdgeSourceImage
         IgnoreSizeMenu.Checked = .DoNotCompareEdgeSize
         RelativeCoordinatesMenu.Checked = .CompareRelativeEdgeCoordinates
         If .WindowPosition IsNot Nothing Then
            Me.Location = .WindowPosition.Value
            Me.StartPosition = FormStartPosition.Manual
         End If
         If .WindowSize IsNot Nothing Then Me.Size = .WindowSize.Value
         ColorMatchMode(.ColorMatchMode)
         ColorTolerance(.ColorTolerance)
         EdgePointDisplaySize(.EdgePointDisplaySize)
         MinimumEdgeLength(.MinimumEdgeLength)
      End With

      DisplayStatus()
   End Sub

   'This procedure toggles whether or not all edges are displayed.
   Private Sub AllEdgesMenu_Click(sender As Object, e As EventArgs) Handles AllEdgesMenu.Click
      UpdateInterface()
   End Sub

   'This procedure closes this program.
   Private Sub CloseProgramMenu_Click(sender As Object, e As EventArgs) Handles CloseProgramMenu.Click
      Me.Close()
   End Sub

   'This procedure gives the command to display the color match mode dialog.
   Private Sub ColorMatchModeMenu_Click(sender As Object, e As EventArgs) Handles ColorMatchModeMenu.Click
      Dim newColorMatchMode As ColorMatchModesE? = Nothing
      Dim newColorMatchModeText As String = Nothing

      newColorMatchModeText = InputBox(ColorMatchModeDescriptions(), , CStr(ColorMatchMode()))

      If Not newColorMatchModeText = Nothing Then
         newColorMatchMode = DirectCast(Integer.Parse(newColorMatchModeText), ColorMatchModesE)
         If ColorMatchMode(newColorMatchMode) Is Nothing Then
            MessageBox.Show("Invalid match mode.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
         End If
      End If
   End Sub

   'This procedure displays the color matching tolerance dialog.
   Private Sub ColorToleranceMenu_Click(sender As Object, e As EventArgs) Handles ColorToleranceMenu.Click
      Dim newColorTolerance As Integer = Nothing
      Dim newColorToleranceText As String = Nothing

      newColorToleranceText = InputBox(String.Format("New tolerance (0-{0}):", MAXIMUM_COLOR_TOLERANCES(ColorMatchMode().Value)), , ColorTolerance().ToString())
      If Not newColorToleranceText = Nothing Then
         If Not (Integer.TryParse(newColorToleranceText, newColorTolerance) AndAlso ColorTolerance(newColorTolerance) IsNot Nothing) Then
            MessageBox.Show("The current color tolerance does not match the match mode and has been reset to the default value.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
         End If
      End If
   End Sub

   'This procedure initializes a comparison between the selected edge and another edge.
   Private Sub CompareMenu_Click(sender As Object, e As EventArgs) Handles CompareMenu.Click
      If Edges().Any Then
         If EdgeSelection().Compare1 Is Nothing Then
            EdgeSelection(EdgeSelectionActionsE.SetCompared1)
            With EdgeSelection()
               MessageBox.Show($"Select another edge to compare edge #{ .Compare1} with and select compare again.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End With
         ElseIf EdgeSelection().Compare2 Is Nothing Then
            EdgeSelection(EdgeSelectionActionsE.SetCompared2)
            With EdgeSelection()
               MessageBox.Show($"The difference between edges #{ .Compare1.Value + 1} and #{ .Compare2.Value + 1} is: {EdgeSelection(EdgeSelectionActionsE.Compare).Difference} pixels.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End With
         End If

         UpdateInterface()
      End If
   End Sub

   'This procedure copies the selected ege to the clipboard.
   Private Sub CopyMenu_Click(sender As Object, e As EventArgs) Handles CopyMenu.Click
      Dim currentEdge As New List(Of Point)

      If Edges().Any Then
         CurrentEdge = Edges()(EdgeSelection().Current)
         Clipboard.SetDataObject(CurrentEdge, copy:=True)
      End If
   End Sub

   'This procedure copies the selected ege to the clipboard and the removes it from the current set of edges.
   Private Sub CutMenu_Click(sender As Object, e As EventArgs) Handles CutMenu.Click
      If Edges().Any Then
         CopyMenu.PerformClick()
         DeleteMenu.PerformClick()
      End If
   End Sub

   'This procedure gives the command to delete the selected edge.
   Private Sub DeleteMenu_Click(sender As Object, e As EventArgs) Handles DeleteMenu.Click
      If Edges().Any Then
         Edges(EdgeActionsE.Delete, , edgeIndex:=EdgeSelection().Current)
         If EdgeSelection().Current >= Edges().Count Then EdgeSelection(EdgeSelectionActionsE.PreviousEdge)
         UpdateInterface()
      End If
   End Sub

   'This procedure gives the command to delete all edges.
   Private Sub DeleteAllMenu_Click(sender As Object, e As EventArgs) Handles DeleteAllMenu.Click
      EdgeFinder(, , ImageBox.Image, refresh:=True)
      UpdateInterface()
   End Sub

   'This procedure gives the command to display the edge highlight color dialog.
   Private Sub EdgeHighlightColorMenu_Click(sender As Object, e As EventArgs) Handles EdgeHighlightColorMenu.Click
      EdgeHighlightColorDialog.ShowDialog()
      UpdateInterface()
   End Sub

   'This procedure gives the command to display the edge point size dialog.
   Private Sub EdgePointDisplaySizeMenu_Click(sender As Object, e As EventArgs) Handles EdgePointDisplaySizeMenu.Click
      Dim newEdgePointDisplaySize As New Integer
      Dim newEdgePointDisplaySizeText As String = Nothing

      newEdgePointDisplaySizeText = InputBox("New edge point display size:", , EdgePointDisplaySize().ToString())
      If Not newEdgePointDisplaySizeText = Nothing Then
         Integer.TryParse(newEdgePointDisplaySizeText, newEdgePointDisplaySize)
         If EdgePointDisplaySize(newEdgePointDisplaySize) Is Nothing Then
            MessageBox.Show("Invalid edge point display size.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
         End If
      End If

      UpdateInterface()
   End Sub

   'This procedure gives the command to display the edges' display color dialog.
   Private Sub EdgesDisplayColorMenu_Click(sender As Object, e As EventArgs) Handles EdgesDisplayColorMenu.Click
      EdgesDisplayColorDialog.ShowDialog()
      UpdateInterface()
   End Sub

   'This procedure toggles whether or not the edge source image will be displayed.
   Private Sub EdgeSourceImageMenu_Click(sender As Object, e As EventArgs) Handles EdgeSourceImageMenu.Click
      UpdateInterface()
   End Sub

   'This procedure gives the command to export the edges that have been extracted from the current image.
   Private Sub ExportEdgesMenu_Click(sender As Object, e As EventArgs) Handles ExportEdgesMenu.Click
      Static exportDialog As New SaveFileDialog

      With ExportDialog
         .AddExtension = True
         .Filter = EDGE_FILE_FILTER

         If .ShowDialog() = DialogResult.OK Then ExportEdges(.FileName)
      End With
   End Sub

   'This procedure gives the command to extract edges from a source image.
   Private Sub ExtractFromImageMenu_Click(sender As Object, e As EventArgs) Handles ExtractFromImageMenu.Click
      DisplayStatus("Extracting edges from entire image. - Press Escape to cancel...")
      MousePointer(Busy:=True)
      AllEdgesMenu.Checked = True
      EdgeSourceImageMenu.Checked = False
      EdgeFinder(, , EdgeSourceImage())
      EdgeSelection(EdgeSelectionActionsE.GoToFirst)
      UpdateInterface()
      MousePointer(Busy:=False)
   End Sub

   'This procedure gives the command to display the help.
   Private Sub HelpMenu_Click(sender As Object, e As EventArgs) Handles HelpMenu.Click
      HelpWindow.ShowDialog()
   End Sub

   'This procedure handles the user's single mouse clicks.
   Private Sub ImageBox_MouseClick(sender As Object, e As MouseEventArgs) Handles ImageBox.MouseClick
      Dim newSelection As New Integer?

      If e.Button = MouseButtons.Left Then
         If My.Computer.Keyboard.ShiftKeyDown Then
            DisplayStatus("Extracting edges from the selected image are... - Press Escape to cancel...")
            MousePointer(Busy:=True)

            Do While My.Computer.Keyboard.ShiftKeyDown
               My.Application.DoEvents()
            Loop
            EdgeFinder(e.X, e.Y, EdgeSourceImage())
            EdgeSelection(EdgeSelectionActionsE.GoToFirst)

            UpdateInterface()
            MousePointer(Busy:=False)
         Else
            newSelection = EdgeAt(New Point(e.X, e.Y))
            If NewSelection IsNot Nothing Then
               EdgeSelection(EdgeSelectionActionsE.SelectEdge, NewSelection.Value)
               UpdateInterface()
            End If
         End If
      End If
   End Sub

   'This procedure handles the user's double mouse clicks.
   Private Sub ImageBox_DoubleClick(sender As Object, e As MouseEventArgs) Handles ImageBox.MouseDoubleClick
      If e.Button = MouseButtons.Left Then
         If Edges().Any Then
            Edges(EdgeActionsE.Replace, edge:=MoveEdge(Edges()(EdgeSelection().Current), New Point(e.X, e.Y)), edgeSet:=Nothing, edgeIndex:=EdgeSelection().Current)
            UpdateInterface()
         End If
      End If
   End Sub

   'This procedure gives the command to draw the current edge over the current image.
   Private Sub ImageBox_Paint(sender As Object, e As PaintEventArgs) Handles ImageBox.Paint
      If EdgeSourceImageMenu.Checked Then
         ImageBox.Size = EdgeSourceImage().Size()
         e.Graphics.DrawImage(EdgeSourceImage(), New Rectangle(0, 0, EdgeSourceImage().Width, EdgeSourceImage().Height))
      End If
      If Edges().Any Then DrawEdges(e.Graphics)
   End Sub

   'This procedure gives the command to import edges.
   Private Sub ImportEdgesMenu_Click(sender As Object, e As EventArgs) Handles ImportEdgesMenu.Click
      Dim importedEdges As List(Of List(Of Point)) = Nothing
      Static importDialog As New OpenFileDialog

      With ImportDialog
         .Filter = EDGE_FILE_FILTER

         If .ShowDialog() = DialogResult.OK Then
            ImportedEdges = ImportEdges(.FileName)

            If ImportedEdges Is Nothing Then
               MessageBox.Show($"""{importDialog.FileName}"" is not a valid edge file.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
               Edges(EdgeActionsE.ReplaceAll, , EdgeSet:=ImportedEdges)
            End If

            UpdateInterface()
         End If
      End With
   End Sub

   'This procedure displays information about this program.
   Private Sub InformationMenu_Click(sender As Object, e As EventArgs) Handles InformationMenu.Click
      MessageBox.Show(My.Application.Info.Description, ProgramInformation, MessageBoxButtons.OK, MessageBoxIcon.Information)
   End Sub

   'This procedure gives the command to load the image file dropped in the interface window.
   Private Sub InterfaceWindow_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
      Try
         If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            EdgeSourceImage(, newSourceImage:=New Bitmap(DirectCast(e.Data.GetData(DataFormats.FileDrop), String()).First()))
            UpdateInterface()
         End If
      Catch ExceptionObj As Exception
         DisplayError(ExceptionObj)
      End Try
   End Sub

   'This procedure handles objects being dragged into the interface window.
   Private Sub InterfaceWindow_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
      If e.Data.GetDataPresent(DataFormats.FileDrop) Then e.Effect = DragDropEffects.All
   End Sub

   'This procedure is executed when this window is closed.
   Private Sub InterfaceWindow_FormClosed(sender As Object, e As EventArgs) Handles Me.FormClosed
      Dim newUserSettings As New UserSettingsClass

      EdgeFinderActive(NewEdgeFinderActive:=False)

      With newUserSettings
         .ColorMatchMode = ColorMatchMode().Value
         .ColorTolerance = ColorTolerance().Value
         .CompareRelativeEdgeCoordinates = RelativeCoordinatesMenu.Checked
         .DisplayEdgeSourceImage = EdgeSourceImageMenu.Checked
         .DoNotCompareEdgeSize = IgnoreSizeMenu.Checked
         .EdgeHighlightColor = EdgeHighlightColorDialog.Color
         .EdgePointDisplaySize = EdgePointDisplaySize().Value
         .EdgesDisplayColor = EdgesDisplayColorDialog.Color
         .MinimumEdgeLength = MinimumEdgeLength().Value
         .ViewAllEdges = AllEdgesMenu.Checked
         .WindowPosition = Me.Location
         .WindowSize = Me.Size
      End With

      SaveUserSettings(NewUserSettings)
   End Sub

   'This procedure handles the user's key strokes.
   Private Sub InterfaceWindow_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
      If EdgeFinderActive() AndAlso e.KeyCode = Keys.Escape Then
         EdgeFinderActive(newEdgeFinderActive:=False)
      Else
         If Edges().Any Then
            Select Case e.KeyCode
               Case Keys.Down
                  EdgeSelection(EdgeSelectionActionsE.PreviousEdge)
               Case Keys.End
                  EdgeSelection(EdgeSelectionActionsE.GoToLast)
               Case Keys.Home
                  EdgeSelection(EdgeSelectionActionsE.GoToFirst)
               Case Keys.Up
                  EdgeSelection(EdgeSelectionActionsE.NextEdge)
               Case Else
                  Exit Sub
            End Select
         End If

         UpdateInterface()
      End If
   End Sub

   'This procedure allows the user to select and load an image.
   Private Sub LoadSourceImageMenu_Click(sender As Object, e As EventArgs) Handles LoadSourceImageMenu.Click
      Try
         With New OpenFileDialog
            Dim filter As New StringBuilder("All supported images|")

            IMAGE_EXTENSIONS.ForEach(Sub(Extension As String) filter.Append(String.Format("*{0};", Extension)))
            filter.Append("|(*.*) all files|*.*")

            .Filter = filter.ToString()
            If .ShowDialog() = DialogResult.OK Then
               EdgeSourceImage(, New Bitmap(.FileName))
               AllEdgesMenu.Checked = True
               EdgeSourceImageMenu.Checked = True
               UpdateInterface()
            End If
         End With
      Catch ExceptionObj As Exception
         DisplayError(ExceptionObj)
      End Try
   End Sub

   'This procedure gives the command to display the minimum edge length dialog.
   Private Sub MinimumEdgeLengthMenu_Click(sender As Object, e As EventArgs) Handles MinimumEdgeLengthMenu.Click
      Dim newMinimumEdgeLength As New Integer
      Dim newMinimumEdgeLengthText As String = Nothing

      newMinimumEdgeLengthText = InputBox("New minimum edge length:", , MinimumEdgeLength().ToString())

      If Not newMinimumEdgeLengthText = Nothing Then
         Integer.TryParse(newMinimumEdgeLengthText, newMinimumEdgeLength)
         If MinimumEdgeLength(newMinimumEdgeLength) Is Nothing Then
            MessageBox.Show("Invalid minimum edge length.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
         End If
      End If
   End Sub

   'This procedure gives the command to horizontally mirror the selected edge.
   Private Sub MirrorHorizontallyMenu_Click(sender As Object, e As EventArgs) Handles MirrorHorizontallyMenu.Click
      If Edges().Any Then
         EdgesFoundMap(, DirectCast(Edges()(EdgeSelection().Current), List(Of Point)), isEdgePoint:=False)
         Edges(EdgeActionsE.Replace, ModifyEdge(Edges()(EdgeSelection().Current), EdgeModifiersE.MirrorHorizontal), edgeSet:=Nothing, edgeIndex:=EdgeSelection().Current)
         EdgesFoundMap(, DirectCast(Edges()(EdgeSelection().Current), List(Of Point)), isEdgePoint:=True)
         UpdateInterface()
      End If
   End Sub

   'This procedure gives the command to vertically mirror the selected edge.
   Private Sub MirrorVerticallyMenu_Click(sender As Object, e As EventArgs) Handles MirrorVerticallyMenu.Click
      If Edges().Any Then
         EdgesFoundMap(, DirectCast(Edges()(EdgeSelection().Current), List(Of Point)), isEdgePoint:=False)
         Edges(EdgeActionsE.Replace, ModifyEdge(Edges()(EdgeSelection().Current), EdgeModifiersE.MirrorVertical), edgeSet:=Nothing, edgeIndex:=EdgeSelection().Current)
         EdgesFoundMap(, DirectCast(Edges()(EdgeSelection().Current), List(Of Point)), isEdgePoint:=True)
         UpdateInterface()
      End If
   End Sub

   'This procedure gives the command to move the selected edge specified by the user.
   Private Sub MoveMenu_Click(sender As Object, e As EventArgs) Handles MoveMenu.Click
      If Edges().Any Then
         Dim rectangleO As Rectangle = EdgeRectangle(Edges()(EdgeSelection().Current))
         Dim newXYText As String = $"{rectangleO.X},{rectangleO.Y}"

         ImageBox.CreateGraphics.DrawRectangle(New Pen(Color.Black) With {.DashStyle = DashStyle.Dot}, rectangleO)

         newXYText = InputBox("Specify new position for edge:",, newXYText)
         If Not newXYText = Nothing Then
            With newXYText.Split(","c)
               If Integer.TryParse(.First, rectangleO.X) AndAlso Integer.TryParse(.Last, rectangleO.Y) AndAlso rectangleO.X >= 0 AndAlso rectangleO.Y >= 0 Then
                  Edges(EdgeActionsE.Replace, edge:=MoveEdge(Edges()(EdgeSelection().Current), New Point(rectangleO.X, rectangleO.Y)), edgeSet:=Nothing, edgeIndex:=EdgeSelection().Current)
               Else
                  MessageBox.Show("Invalid position.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
               End If
            End With
         End If

         UpdateInterface()
      End If
   End Sub

   'This procedure adds any edge present on the clipboard to the current set of edges.
   Private Sub PasteMenu_Click(sender As Object, e As EventArgs) Handles PasteMenu.Click
      Dim newEdge As Object = Clipboard.GetDataObject().GetData((New List(Of Point)).GetType)

      If newEdge Is Nothing Then
         MessageBox.Show("No edge found on clipboard.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      Else
         Edges(EdgeActionsE.Add,, New List(Of List(Of Point))({New List(Of Point)(DirectCast(NewEdge, List(Of Point)))}))
         EdgesFoundMap(, DirectCast(newEdge, List(Of Point)), isEdgePoint:=True)
         UpdateInterface()
      End If
   End Sub

   'This procedure sets any image present on the clipboard as the edge source image.
   Private Sub PasteSourceImageMenu_Click(sender As Object, e As EventArgs) Handles PasteSourceImageMenu.Click
      If My.Computer.Clipboard.ContainsImage() Then
         EdgeSourceImage(getFromClipboard:=True)
         AllEdgesMenu.Checked = True
         EdgeSourceImageMenu.Checked = True
         UpdateInterface()
      Else
         MessageBox.Show("No image found on clipboard.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      End If
   End Sub

   'This procedure rotates the selected edge.
   Private Sub RotateOnSideMenu_Click(sender As Object, e As EventArgs) Handles RotateOnSideMenu.Click
      EdgesFoundMap(,,,, rotateOnSide:=True)
      Edges(EdgeActionsE.Replace, ModifyEdge(Edges()(EdgeSelection().Current), EdgeModifiersE.RotateOnSide), edgeSet:=Nothing, edgeIndex:=EdgeSelection().Current)
      UpdateInterface()
   End Sub

   'This procedure sorts the current edges by similarity.
   Private Sub SortBySimilarityMenu_Click(sender As Object, e As EventArgs) Handles SortBySimilarityMenu.Click
      Dim compareOptions As EdgeCompareOptionsE = EdgeCompareOptionsE.DefaultO
      Dim edgeDifferences As New List(Of EdgeDifferenceStr)

      MousePointer(Busy:=True)

      If IgnoreSizeMenu.Checked Then CompareOptions = CompareOptions Or EdgeCompareOptionsE.IgnoreSize
      If RelativeCoordinatesMenu.Checked Then CompareOptions = CompareOptions Or EdgeCompareOptionsE.Relative

      For otherEdge As Integer = 0 To Edges().Count - 1
         edgeDifferences.Add(New EdgeDifferenceStr With {.Difference = CompareEdges(EdgeSelection().Current, otherEdge, compareOptions), .Edge = New List(Of Point)(Edges()(otherEdge))})
      Next otherEdge

      Edges(EdgeActionsE.ReplaceAll,, (From EdgeDifference In EdgeDifferences Order By EdgeDifference.Difference Ascending Select EdgeDifference.Edge).ToList())

      MousePointer(Busy:=False)

      MessageBox.Show("Done sorting edges.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)

      UpdateInterface()
   End Sub

   'This procedure displays the current status information.
   Private Sub DisplayStatus(Optional status As String = Nothing)
      If status Is Nothing Then
         If Edges().Any Then
            With EdgeSelection()
               EdgeStatusLabel.Text = $"Edge index: { .Current + 1}/{Edges().Count}   Length: {Edges()(.Current).Count}"
               If .Compare1 IsNot Nothing OrElse .Compare2 IsNot Nothing Then
                  EdgeStatusLabel.Text &= "   Select another edge and select ""Compare""."
               End If
            End With
         Else
            EdgeStatusLabel.Text = "No edges."
         End If
      Else
         EdgeStatusLabel.Text = status
      End If

      Application.DoEvents()
   End Sub

   'This procedure draws the specified edge.
   Private Sub DrawEdge(canvas As Graphics, edge As List(Of Point), colorO As SolidBrush)
      edge.ForEach(Sub(EdgePoint As Point) canvas.FillRectangle(colorO, EdgePoint.X, EdgePoint.Y, EdgePointDisplaySize().Value, EdgePointDisplaySize().Value))
   End Sub

   'This procedure draws either the current edges or all edges.
   Private Sub DrawEdges(canvas As Graphics)
      If AllEdgesMenu.Checked Then
         Edges().ForEach(Sub(Edge As List(Of Point)) DrawEdge(canvas, Edge, New SolidBrush(EdgesDisplayColorDialog.Color)))
      End If

      DrawEdge(canvas, Edges()(EdgeSelection().Current), New SolidBrush(EdgeHighlightColorDialog.Color))
   End Sub

   'This procedure returns the index of the edge located at the specified position.
   Private Function EdgeAt(atXY As Point) As Integer?
      Dim index As New Integer?
      Dim size As Integer = EdgePointDisplaySize().Value
      Dim xy As New Point

      For x As Integer = atXY.X - size To atXY.X + size
         For y As Integer = atXY.Y - size To atXY.Y + size
            xy = New Point(x, y)
            index = Edges().FindIndex(Function(Edge As List(Of Point)) Edge.Contains(xy))
            If index >= 0 Then Return index
         Next y
      Next x

      Return Nothing
   End Function

   'This procedure manages the edge selection information.
   Private Function EdgeSelection(Optional action As EdgeSelectionActionsE = EdgeSelectionActionsE.None, Optional newSelection As Integer = Nothing) As EdgeSelectionStr
      Dim compareOptions As EdgeCompareOptionsE = EdgeCompareOptionsE.DefaultO
      Static selection As New EdgeSelectionStr With {.Compare1 = Nothing, .Compare2 = Nothing, .Difference = 0, .Current = 0}

      If selection.Current >= Edges().Count Then
         selection = New EdgeSelectionStr With {.Compare1 = Nothing, .Compare2 = Nothing, .Difference = 0, .Current = 0}
      End If

      With selection
         Select Case action
            Case EdgeSelectionActionsE.Compare
               If IgnoreSizeMenu.Checked Then compareOptions = compareOptions Or EdgeCompareOptionsE.IgnoreSize
               If RelativeCoordinatesMenu.Checked Then compareOptions = compareOptions Or EdgeCompareOptionsE.Relative

               .Difference = CompareEdges(.Compare1.Value, .Compare2.Value, compareOptions)
               .Compare1 = Nothing
               .Compare2 = Nothing
            Case EdgeSelectionActionsE.GoToFirst
               .Current = 0
            Case EdgeSelectionActionsE.GoToLast
               .Current = Edges().Count - 1
            Case EdgeSelectionActionsE.NextEdge
               If .Current < Edges().Count - 1 Then .Current += 1
            Case EdgeSelectionActionsE.PreviousEdge
               If .Current > 0 Then .Current -= 1
            Case EdgeSelectionActionsE.SelectEdge
               .Current = newSelection
            Case EdgeSelectionActionsE.SetCompared1
               .Compare1 = .Current
            Case EdgeSelectionActionsE.SetCompared2
               .Compare2 = .Current
         End Select
      End With

      Return selection
   End Function

   'This procedure manages the source image from which edges are extracted.
   Private Function EdgeSourceImage(Optional getFromClipboard As Boolean = False, Optional newSourceImage As Image = Nothing, Optional clear As Boolean = False) As Image
      Static currentSourceImage As Image = Nothing

      If clear Then
         currentSourceImage = New Bitmap(ImageBox.Width, ImageBox.Height)
      ElseIf getFromClipboard Then
         Edges(EdgeActionsE.DeleteAll)
         currentSourceImage = My.Computer.Clipboard.GetImage()
      ElseIf newSourceImage IsNot Nothing Then
         Edges(EdgeActionsE.DeleteAll)
         currentSourceImage = newSourceImage
      End If

      Return currentSourceImage
   End Function

   'This procedure manages the mouse pointer.
   Private Sub MousePointer(busy As Boolean)
      Me.Cursor = If(busy, Cursors.WaitCursor, Cursors.Default)
      Application.DoEvents()

      For Each controlObj As Control In Me.Controls
         controlObj.Cursor = If(busy, Cursors.WaitCursor, Cursors.Default)
         Application.DoEvents()
      Next controlObj
   End Sub

   'This produre this procedure updates this interface window.
   Private Sub UpdateInterface()
      DisplayStatus()
      ImageBox.Invalidate()
   End Sub
End Class
