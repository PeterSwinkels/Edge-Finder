'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Math
Imports System.Windows.Forms

'This module contains edge handling related procedures.
Public Module EdgesModule
   'This enumeration lists the actions that can be performed on edges.
   Public Enum EdgeActionsE As Integer
      None           'Indicates that no action is performed.
      Add            'Adds the specified edges.
      Delete         'Removes the edge with the specified index.
      DeleteAll      'Deletes all edges.
      Replace        'Replaces the edge with the specified index.
      ReplaceAll     'Replaces the current edges with new edges.
   End Enum

   'This enumaration lists the edge compare options.
   Public Enum EdgeCompareOptionsE As Integer
      DefaultO = 0     'Uses the default options.
      IgnoreSize = 1   'Ignores the size difference.
      Relative = 2     'Uses coordinates relative to an edge's rectangle.
   End Enum

   'This enumeration lists the columns found in an edges file.
   Private Enum EdgeIXYE As Integer
      Index    'An edge's index.
      X        'An edge point's horizontal position.
      Y        'An edge point's vertical position.
   End Enum

   Public Const EDGE_FILE_FILTER As String = "(*.csv) Edge files|*.csv|(*.*) All files|*.*"  'Defines the extension file export/import dialog's filter.
   Private Const EDGE_FILE_COLUMNS As String = "Edge:;x:;y:"                                  'Defines the column descriptions for an edge file.
   Private Const EDGE_FILE_FOOTER As String = "-1;0;0"                                        'Defines the footer used for edge files.
   Private Const EDGE_FILE_HEADER As String = "[EDGE FILE];;"                                 'Defines the header used for edge files to be able distinguish them from other files.

   'This procedure returns the average difference in position for the two specified edges' points.
   Public Function CompareEdges(edgeIndex1 As Integer, edgeIndex2 As Integer, compareOption As EdgeCompareOptionsE) As Double
      Dim averageDistance As Double = 0
      Dim averagePointCount As New Double
      Dim distance As New Double
      Dim edge1 As New List(Of Point)(Edges()(edgeIndex1))
      Dim edge2 As New List(Of Point)(Edges()(edgeIndex2))
      Dim edge1Rectangle As Rectangle = Nothing
      Dim edge2Rectangle As Rectangle = Nothing
      Dim edgePoint1 As New Integer
      Dim longestEdgeIndex As New Integer
      Dim lowestDistance As Double? = Nothing
      Dim nearestPoint As Integer? = Nothing

      If edge1.Any AndAlso edge2.Any Then
         If (compareOption And EdgeCompareOptionsE.IgnoreSize) = EdgeCompareOptionsE.IgnoreSize Then
            edge1Rectangle = EdgeRectangle(edge1)
            edge2Rectangle = EdgeRectangle(edge2)
            If SurfaceArea(edge1Rectangle) <= SurfaceArea(edge2Rectangle) Then
               edge1 = ResizeEdge(edge1, edge2Rectangle)
            Else
               edge2 = ResizeEdge(edge2, edge1Rectangle)
            End If
         End If

         If (compareOption And EdgeCompareOptionsE.Relative) = EdgeCompareOptionsE.Relative Then
            edge1 = MoveEdge(edge1, New Point(0, 0))
            edge2 = MoveEdge(edge2, New Point(0, 0))
         End If

         averagePointCount = (edge1.Count + edge2.Count) / 2
         longestEdgeIndex = If(edge1.Count > edge2.Count, 0, 1)

         Do While edge1.Any AndAlso edge2.Any
            edgePoint1 = 0
            Do While edgePoint1 < edge1.Count AndAlso edge2.Count > 0
               nearestPoint = Nothing
               lowestDistance = Nothing
               For edgePoint2 As Integer = 0 To edge2.Count - 1
                  distance = PointDistance(edge1(edgePoint1), edge2(edgePoint2))
                  If lowestDistance Is Nothing OrElse distance <= lowestDistance Then
                     lowestDistance = distance
                     nearestPoint = edgePoint2
                     If lowestDistance < 1 Then Exit For
                  End If
               Next edgePoint2

               averageDistance += lowestDistance.Value

               If longestEdgeIndex = 0 Then
                  edge1.RemoveAt(edgePoint1)
                  Exit Do
               ElseIf longestEdgeIndex = 1 Then
                  edge2.RemoveAt(nearestPoint.Value)
               End If

               edgePoint1 += 1
            Loop
         Loop

         Return averageDistance / averagePointCount
      End If

      Return Nothing
   End Function

   'This procedure gives the command to search the specified image for edges.
   Public Sub EdgeFinder(Optional x As Integer? = Nothing, Optional y As Integer? = Nothing, Optional imageO As Image = Nothing, Optional refresh As Boolean = False)
      Dim currentEdgesFoundMap(,) As Boolean = {{}}
      Dim searchMap(,) As Boolean = {{}}

      EdgeFinderActive(NewEdgeFinderActive:=True)

      With imageO
         If refresh Then
            Edges(EdgeActionsE.DeleteAll)
            ReDim currentEdgesFoundMap(0 To .Width, 0 To .Height)
         ElseIf x Is Nothing AndAlso y Is Nothing Then
            Edges(EdgeActionsE.DeleteAll)
            ReDim currentEdgesFoundMap(0 To .Width, 0 To .Height)
            ReDim searchMap(0 To .Width, 0 To .Height)

            For x = 0 To .Width - 1
               If Not EdgeFinderActive() Then Exit For
               For y = 0 To .Height - 1
                  If Not EdgeFinderActive() Then Exit For
                  If Not searchMap(x.Value, y.Value) Then
                     Edges(EdgeActionsE.Add, , EdgeSet:=GroupNeighbouringPoints(GetEdgePoints(currentEdgesFoundMap, searchMap, imageO, x.Value, y.Value)))
                  End If
               Next y
            Next x

            Edges(EdgeActionsE.ReplaceAll, , EdgeSet:=UngroupUnconnectedEdges(Edges()))
         Else
            currentEdgesFoundMap = EdgesFoundMap()
            ReDim searchMap(0 To .Width, 0 To .Height)

            If currentEdgesFoundMap.GetUpperBound(0) <> .Width OrElse currentEdgesFoundMap.GetUpperBound(1) <> .Height Then
               ReDim currentEdgesFoundMap(0 To .Width, 0 To .Height)
            End If

            Edges(EdgeActionsE.Add, , EdgeSet:=UngroupUnconnectedEdges(GroupNeighbouringPoints(GetEdgePoints(currentEdgesFoundMap, searchMap, imageO, x.Value, y.Value))))
         End If

         EdgesFoundMap(newEdgesFoundMap:=currentEdgesFoundMap)
      End With

      EdgeFinderActive(NewEdgeFinderActive:=False)
   End Sub

   'This procedure manages the edge finder's status.
   Public Function EdgeFinderActive(Optional newEdgeFinderActive As Boolean? = Nothing) As Boolean
      Static currentEdgeFinderActive As Boolean = False

      If newEdgeFinderActive IsNot Nothing Then
         currentEdgeFinderActive = newEdgeFinderActive.Value
      Else
         Application.DoEvents()
      End If

      Return currentEdgeFinderActive
   End Function

   'This procedure manages the edge point display size.
   Public Function EdgePointDisplaySize(Optional newEdgePointDisplaySize As Integer? = Nothing) As Integer?
      Static currentEdgePointDisplaySize As Integer = 2

      If newEdgePointDisplaySize IsNot Nothing Then
         If newEdgePointDisplaySize > 0 Then
            currentEdgePointDisplaySize = newEdgePointDisplaySize.Value
         Else
            Return Nothing
         End If
      End If

      Return currentEdgePointDisplaySize
   End Function

   'This procedure manages the set of edges extracted from the source image.
   Public Function Edges(Optional edgeAction As EdgeActionsE = EdgeActionsE.None, Optional edge As List(Of Point) = Nothing, Optional edgeSet As List(Of List(Of Point)) = Nothing, Optional edgeIndex? As Integer = Nothing) As List(Of List(Of Point))
      Static currentEdges As New List(Of List(Of Point))

      Select Case edgeAction
         Case EdgeActionsE.Add
            edgeSet = (From NewEdge In edgeSet Where NewEdge.Count >= MinimumEdgeLength()).ToList()
            edgeSet.ForEach(Sub(NewEdge As List(Of Point)) currentEdges.Add(New List(Of Point)(NewEdge)))
         Case EdgeActionsE.Delete
            EdgesFoundMap(, currentEdges(edgeIndex.Value), isEdgePoint:=False)
            currentEdges.RemoveAt(edgeIndex.Value)
         Case EdgeActionsE.DeleteAll
            EdgesFoundMap(,,, deleteAll:=True)
            currentEdges = New List(Of List(Of Point))
         Case EdgeActionsE.Replace
            EdgesFoundMap(, currentEdges(edgeIndex.Value), isEdgePoint:=False)
            currentEdges(edgeIndex.Value) = New List(Of Point)(edge)
            EdgesFoundMap(, currentEdges(edgeIndex.Value), isEdgePoint:=True)
         Case EdgeActionsE.ReplaceAll
            currentEdges = edgeSet
      End Select

      Return currentEdges
   End Function

   'This procedure manages a map of the edges found.
   Public Function EdgesFoundMap(Optional newEdgesFoundMap(,) As Boolean = Nothing, Optional edge As List(Of Point) = Nothing, Optional isEdgePoint As Boolean = True, Optional deleteAll As Boolean = False, Optional rotateOnSide As Boolean = False) As Boolean(,)
      Dim rotatedEdgesFoundMap(,) As Boolean = {}
      Static currentEdgesFoundMap(,) As Boolean = {}

      If edge IsNot Nothing Then
         edge.ForEach(Sub(EdgePoint As Point) currentEdgesFoundMap(EdgePoint.X, EdgePoint.Y) = isEdgePoint)
      ElseIf deleteAll Then
         Erase currentEdgesFoundMap
      ElseIf newEdgesFoundMap IsNot Nothing Then
         currentEdgesFoundMap = newEdgesFoundMap
      ElseIf rotateOnSide Then
         ReDim rotatedEdgesFoundMap(Max(currentEdgesFoundMap.GetUpperBound(0), currentEdgesFoundMap.GetUpperBound(1)), Max(currentEdgesFoundMap.GetUpperBound(0), currentEdgesFoundMap.GetUpperBound(1)))

         For x As Integer = currentEdgesFoundMap.GetLowerBound(0) To currentEdgesFoundMap.GetUpperBound(0)
            For y As Integer = currentEdgesFoundMap.GetLowerBound(1) To currentEdgesFoundMap.GetUpperBound(1)
               rotatedEdgesFoundMap(y, x) = currentEdgesFoundMap(x, y)
            Next y
         Next x

         currentEdgesFoundMap = rotatedEdgesFoundMap
      End If

      Return currentEdgesFoundMap
   End Function

   'This procedure exports the specified edges to the specified file.
   Public Sub ExportEdges(exportPath As String)
      Try
         Using FileO As New StreamWriter(exportPath)
            FileO.WriteLine(EDGE_FILE_HEADER)
            FileO.WriteLine(EDGE_FILE_COLUMNS)
            For edge As Integer = 0 To Edges().Count - 1
               For Each edgePoint As Point In Edges()(edge)
                  FileO.WriteLine("{0};{1};{2}", edge, edgePoint.X, edgePoint.Y)
               Next edgePoint
            Next edge
            FileO.WriteLine(EDGE_FILE_FOOTER)
         End Using
      Catch ExceptionObj As Exception
         DisplayError(ExceptionObj)
      End Try
   End Sub

   'This procedure returns edge points of the area at the specified location.
   Private Function GetEdgePoints(edgeFound As Boolean(,), searched(,) As Boolean, imageO As Image, x As Integer, y As Integer) As List(Of Point)
      Dim edgePoints As New List(Of Point)

      With DirectCast(imageO, Bitmap)
         Dim areaColor As Color = .GetPixel(x, y)
         Dim matchMode As ColorMatchModesE = ColorMatchMode().Value
         Dim newNodes As New List(Of Point)
         Dim nodes As New List(Of Point)({New Point(x, y)})
         Dim tolerance As Integer = ColorTolerance().Value

         Do
            newNodes.Clear()
            For Each node As Point In nodes
               If Not searched(node.X, node.Y) AndAlso ColorsMatch(.GetPixel(node.X, node.Y), areaColor, matchMode, tolerance) Then searched(node.X, node.Y) = True
               For Each checkNode As Point In {New Point(node.X - 1, node.Y), New Point(node.X + 1, node.Y), New Point(node.X, node.Y - 1), New Point(node.X, node.Y + 1)}
                  If EdgeFinderActive() Then
                     If checkNode.X >= 0 AndAlso checkNode.X < .Width AndAlso checkNode.Y >= 0 AndAlso checkNode.Y < .Height Then
                        If Not searched(checkNode.X, checkNode.Y) Then
                           If ColorsMatch(.GetPixel(checkNode.X, checkNode.Y), areaColor, matchMode, tolerance) Then
                              searched(checkNode.X, checkNode.Y) = True
                              newNodes.Add(checkNode)
                           ElseIf Not edgeFound(checkNode.X, checkNode.Y) Then
                              edgePoints.Add(checkNode)
                              edgeFound(checkNode.X, checkNode.Y) = True
                           End If
                        End If
                     End If
                  Else
                     Return New List(Of Point)
                  End If
               Next checkNode
            Next node
            If newNodes.Any Then nodes = New List(Of Point)(newNodes)
         Loop While newNodes.Any
      End With

      Return edgePoints.Distinct().ToList()
   End Function

   'This procedure returns the specified point's neighbouring point in the specified edge points list.
   Private Function GetNeighbouringPoint(edgePoints As List(Of Point), referencePoint As Point) As Integer?
      For edgePoint As Integer = 0 To edgePoints.Count - 1
         With edgePoints(edgePoint)
            If Not (.X = referencePoint.X AndAlso .Y = referencePoint.Y) Then
               If .X >= referencePoint.X - 1 AndAlso .X <= referencePoint.X + 1 AndAlso .Y >= referencePoint.Y - 1 AndAlso .Y <= referencePoint.Y + 1 Then
                  Return edgePoint
               End If
            End If
         End With
      Next edgePoint

      Return Nothing
   End Function

   'This procedure groups the specified edge points that are neighbours of each other.
   Private Function GroupNeighbouringPoints(ungroupedEdgePoints As List(Of Point)) As List(Of List(Of Point))
      Dim groupedEdgePoints As New List(Of List(Of Point))
      Dim neighbouringPoint As New Integer?
      Dim neighbouringPoints As New List(Of Point)

      With ungroupedEdgePoints
         If .Any Then
            neighbouringPoints.Add(.First())
            .RemoveAt(0)
            Do While .Any AndAlso EdgeFinderActive()
               neighbouringPoint = GetNeighbouringPoint(ungroupedEdgePoints, neighbouringPoints.Last())

               If neighbouringPoint Is Nothing Then
                  groupedEdgePoints.Add(New List(Of Point)(neighbouringPoints))
                  neighbouringPoints = New List(Of Point)({ .First()})
                  .RemoveAt(0)
               Else
                  neighbouringPoints.Add(.Item(neighbouringPoint.Value))
                  .RemoveAt(neighbouringPoint.Value)
               End If

               If Not .Any Then groupedEdgePoints.Add(New List(Of Point)(neighbouringPoints))
            Loop
         End If
      End With

      Return groupedEdgePoints
   End Function

   'This procedure imports the specified edges file and returns the result.
   Public Function ImportEdges(importPath As String) As List(Of List(Of Point))
      Try
         Dim currentEdge As Integer = 0
         Dim edge As New List(Of Point)
         Dim edgeIXY() As String = {}
         Dim edgeIXYs As New List(Of String)
         Dim importedEdges As New List(Of List(Of Point))
         Dim previousEdge As Integer = currentEdge

         edgeIXYs = New List(Of String)(File.ReadAllLines(importPath))
         If edgeIXYs.First = EDGE_FILE_HEADER Then
            edgeIXYs.RemoveAt(0)
            edgeIXYs.RemoveAt(0)
            For Each edgeLine As String In edgeIXYs
               edgeIXY = edgeLine.Split(";"c)
               previousEdge = currentEdge
               currentEdge = Integer.Parse(edgeIXY(EdgeIXYE.Index))
               If Not currentEdge = previousEdge Then
                  importedEdges.Add(New List(Of Point)(edge))
                  edge.Clear()
               End If
               edge.Add(New Point(Integer.Parse(edgeIXY(EdgeIXYE.X)), Integer.Parse(edgeIXY(EdgeIXYE.Y))))
            Next edgeLine

            Return importedEdges
         End If
      Catch ExceptionObj As Exception
         DisplayError(ExceptionObj)
      End Try

      Return Nothing
   End Function

   'This procedure manages the minimum edge length.
   Public Function MinimumEdgeLength(Optional newMinimumEdgeLength As Integer? = Nothing) As Integer?
      Static currentMinimumEdgeLength As Integer = 2

      If newMinimumEdgeLength IsNot Nothing Then
         If newMinimumEdgeLength > 0 Then
            currentMinimumEdgeLength = newMinimumEdgeLength.Value
         Else
            Return Nothing
         End If
      End If

      Return currentMinimumEdgeLength
   End Function

   'This procedure returns the distance between the two specified points.
   Private Function PointDistance(point1 As Point, point2 As Point) As Double
      Return Sqrt(((point2.X - point1.X) ^ 2) + ((point2.Y - point1.Y) ^ 2))
   End Function

   'This procedure returns the specified rectangle's surface area.
   Private Function SurfaceArea(rectangleO As Rectangle) As Integer
      Return rectangleO.Width * rectangleO.Height
   End Function

   'This procedure ungroups the specified edges that are not connected.
   Private Function UngroupUnconnectedEdges(edges As List(Of List(Of Point))) As List(Of List(Of Point))
      Dim previousCount As New Integer

      Do
         For edgeIndex As Integer = 0 To edges.Count - 1
            If Not EdgeFinderActive() Then Exit Do
            For otherEdgeIndex As Integer = 0 To edges.Count - 1
               If Not (edgeIndex = otherEdgeIndex OrElse edges(otherEdgeIndex).Count = 0) Then
                  With edges(edgeIndex)
                     For edgePoint As Integer = 0 To .Count - 1
                        If GetNeighbouringPoint(edges(otherEdgeIndex), .Item(edgePoint)) IsNot Nothing Then
                           edges(edgeIndex).AddRange(edges(otherEdgeIndex).Distinct())
                           edges(otherEdgeIndex).Clear()
                           Exit For
                        End If
                     Next edgePoint
                  End With
               End If
            Next otherEdgeIndex
         Next edgeIndex
         previousCount = edges.Count
         edges = (From Edge As List(Of Point) In edges Where Edge.Count > 0).ToList()
      Loop Until edges.Count = previousCount

      Return edges
   End Function
End Module
