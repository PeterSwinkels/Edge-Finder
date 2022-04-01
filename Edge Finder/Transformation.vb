'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Math

'This module contains the edge transformation procedures.
Public Module TransformationModule
   'This enumeration lists the edge modification options.
   Public Enum EdgeModifiersE As Integer
      MirrorHorizontal   'Horizontally mirrors an edge.
      MirrorVertical     'Vertically mirrors an edge.
      RotateOnSide       'Rotates and edge on its side.
   End Enum

   'This procedure returns an imaginary rectangle fitting the specified edge.
   Public Function EdgeRectangle(edge As List(Of Point)) As Rectangle
      Dim bottom As Integer = Integer.MinValue
      Dim left As Integer = Integer.MaxValue
      Dim right As Integer = Integer.MinValue
      Dim top As Integer = Integer.MaxValue

      For Each edgePoint As Point In edge
         With edgePoint
            If .X <= left Then left = .X
            If .Y <= top Then top = .Y
            If .X >= right Then right = .X
            If .Y >= bottom Then bottom = .Y
         End With
      Next edgePoint

      Return New Rectangle(left, top, right - left, bottom - top)
   End Function

   'This procedure mirrors the specified edge horizontally or vertically and returns the result.
   Public Function ModifyEdge(edge As List(Of Point), modifier As EdgeModifiersE) As List(Of Point)
      Dim ModifiedEdge As New List(Of Point)

      With EdgeRectangle(edge)
         Select Case modifier
            Case EdgeModifiersE.MirrorHorizontal
               edge.ForEach(Sub(EdgePoint As Point) ModifiedEdge.Add(New Point((.Right - EdgePoint.X) + .Left, EdgePoint.Y)))
            Case EdgeModifiersE.MirrorVertical
               edge.ForEach(Sub(EdgePoint As Point) ModifiedEdge.Add(New Point(EdgePoint.X, (.Bottom - EdgePoint.Y) + .Top)))
            Case EdgeModifiersE.RotateOnSide
               edge.ForEach(Sub(EdgePoint As Point) ModifiedEdge.Add(New Point(EdgePoint.Y, EdgePoint.X)))
         End Select
      End With

      Return ModifiedEdge
   End Function

   'This procedure moves the specified edge to the specified position and returns the result.
   Public Function MoveEdge(edge As List(Of Point), newXY As Point) As List(Of Point)
      Dim currentEdgesFoundMap(,) As Boolean = EdgesFoundMap()
      Dim movedEdge As New List(Of Point)
      Dim newEdgesFoundMap(,) As Boolean = {{}}
      Dim rectangleO As Rectangle = EdgeRectangle(edge)

      edge.ForEach(Sub(EdgePoint As Point) movedEdge.Add(New Point(EdgePoint.X - (rectangleO.X - newXY.X), EdgePoint.Y - (rectangleO.Y - newXY.Y))))

      rectangleO = EdgeRectangle(movedEdge)

      If rectangleO.X + rectangleO.Width >= currentEdgesFoundMap.GetUpperBound(0) OrElse rectangleO.Y + rectangleO.Height >= currentEdgesFoundMap.GetUpperBound(1) Then
         ReDim newEdgesFoundMap(0 To Max(currentEdgesFoundMap.GetUpperBound(0), rectangleO.X + rectangleO.Width), 0 To Max(currentEdgesFoundMap.GetUpperBound(1), rectangleO.Y + rectangleO.Height))
         Array.Copy(currentEdgesFoundMap, newEdgesFoundMap, currentEdgesFoundMap.Length)
         EdgesFoundMap(newEdgesFoundMap:=newEdgesFoundMap)
      End If

      Return movedEdge
   End Function

   'This procedure resizes the specified edge and returns the result.
   Public Function ResizeEdge(edge As List(Of Point), newRectangle As Rectangle) As List(Of Point)
      Dim oldRectangle As Rectangle = EdgeRectangle(edge)
      Dim resizedEdge As New List(Of Point)
      Dim xAdjustment As Double = ((newRectangle.Width + 1) / (oldRectangle.Width + 1))
      Dim yAdjustment As Double = ((newRectangle.Height + 1) / (oldRectangle.Height + 1))

      edge.ForEach(Sub(EdgePoint As Point) resizedEdge.Add(New Point(CInt(EdgePoint.X * xAdjustment), CInt(EdgePoint.Y * yAdjustment))))

      Return MoveEdge(resizedEdge.Distinct().ToList(), New Point(oldRectangle.X, oldRectangle.Y))
   End Function
End Module
