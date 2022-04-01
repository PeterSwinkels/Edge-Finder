<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainInterfaceWindow
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()>
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
   <System.Diagnostics.DebuggerStepThrough()>
   Private Sub InitializeComponent()
      Me.ImageBox = New System.Windows.Forms.PictureBox()
      Me.StatusBar = New System.Windows.Forms.StatusStrip()
      Me.EdgeStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.ImagePanel = New System.Windows.Forms.Panel()
      Me.MenuBar = New System.Windows.Forms.MenuStrip()
      Me.FileMainMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.LoadSourceImageMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.FileMainMenuSeparator1 = New System.Windows.Forms.ToolStripSeparator()
      Me.ExportEdgesMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.ImportEdgesMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.FileMainMenuSeparator2 = New System.Windows.Forms.ToolStripSeparator()
      Me.CloseProgramMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.EdgesMainMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.DeleteAllMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.EdgesMainMenuSeparator1 = New System.Windows.Forms.ToolStripSeparator()
      Me.ExtractFromImageMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.EdgesMainMenuSeparator2 = New System.Windows.Forms.ToolStripSeparator()
      Me.PasteSourceImageMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.EdgesMainMenuSeparator3 = New System.Windows.Forms.ToolStripSeparator()
      Me.SortBySimilarityMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.EdgesMainMenuSeparator4 = New System.Windows.Forms.ToolStripSeparator()
      Me.SelectionSubMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.CompareMenu = New System.Windows.Forms.ToolStripMenuItem()
      Me.SelectionSubMenuSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.CutMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectionSubMenuSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.MirrorHorizontallyMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.MirrorVerticallyMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.RotateOnSideMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewMainMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllEdgesMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.EdgeSourceImageMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsMainMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.EdgeHighlightColorMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.EdgePointDisplaySizeMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.EdgesDisplayColorMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsMainMenuSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ColorMatchModeMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColorToleranceMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.MinimumEdgeLengthMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsMainMenuSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.EdgeCompareModeMEnu = New System.Windows.Forms.ToolStripMenuItem()
        Me.IgnoreSizeMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.RelativeCoordinatesMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpMainMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.InformationMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpMainMenuSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.EdgesDisplayColorDialog = New System.Windows.Forms.ColorDialog()
        Me.EdgeHighlightColorDialog = New System.Windows.Forms.ColorDialog()
        CType(Me.ImageBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusBar.SuspendLayout()
        Me.ImagePanel.SuspendLayout()
        Me.MenuBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageBox
        '
        Me.ImageBox.BackColor = System.Drawing.Color.White
        Me.ImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ImageBox.Location = New System.Drawing.Point(2, 0)
        Me.ImageBox.Margin = New System.Windows.Forms.Padding(2)
        Me.ImageBox.Name = "ImageBox"
        Me.ImageBox.Size = New System.Drawing.Size(75, 41)
        Me.ImageBox.TabIndex = 1
        Me.ImageBox.TabStop = False
        '
        'StatusBar
        '
        Me.StatusBar.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EdgeStatusLabel})
        Me.StatusBar.Location = New System.Drawing.Point(0, 232)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.StatusBar.Size = New System.Drawing.Size(352, 22)
        Me.StatusBar.TabIndex = 1
        '
        'EdgeStatusLabel
        '
        Me.EdgeStatusLabel.Name = "EdgeStatusLabel"
        Me.EdgeStatusLabel.Size = New System.Drawing.Size(0, 17)
        '
        'ImagePanel
        '
        Me.ImagePanel.AutoScroll = True
        Me.ImagePanel.BackColor = System.Drawing.Color.White
        Me.ImagePanel.Controls.Add(Me.ImageBox)
        Me.ImagePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImagePanel.Location = New System.Drawing.Point(0, 24)
        Me.ImagePanel.Margin = New System.Windows.Forms.Padding(2)
        Me.ImagePanel.Name = "ImagePanel"
        Me.ImagePanel.Size = New System.Drawing.Size(352, 208)
        Me.ImagePanel.TabIndex = 2
        '
        'MenuBar
        '
        Me.MenuBar.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMainMenu, Me.EdgesMainMenu, Me.ViewMainMenu, Me.OptionsMainMenu, Me.HelpMainMenu})
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuBar.Size = New System.Drawing.Size(352, 24)
        Me.MenuBar.TabIndex = 3
        '
        'FileMainMenu
        '
        Me.FileMainMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadSourceImageMenu, Me.FileMainMenuSeparator1, Me.ExportEdgesMenu, Me.ImportEdgesMenu, Me.FileMainMenuSeparator2, Me.CloseProgramMenu})
        Me.FileMainMenu.Name = "FileMainMenu"
        Me.FileMainMenu.Size = New System.Drawing.Size(37, 20)
        Me.FileMainMenu.Text = "&File"
        '
        'LoadSourceImageMenu
        '
        Me.LoadSourceImageMenu.Name = "LoadSourceImageMenu"
        Me.LoadSourceImageMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.LoadSourceImageMenu.Size = New System.Drawing.Size(215, 22)
        Me.LoadSourceImageMenu.Text = "&Load Source Image"
        '
        'FileMainMenuSeparator1
        '
        Me.FileMainMenuSeparator1.Name = "FileMainMenuSeparator1"
        Me.FileMainMenuSeparator1.Size = New System.Drawing.Size(212, 6)
        '
        'ExportEdgesMenu
        '
        Me.ExportEdgesMenu.Name = "ExportEdgesMenu"
        Me.ExportEdgesMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.ExportEdgesMenu.Size = New System.Drawing.Size(215, 22)
        Me.ExportEdgesMenu.Text = "&Export Edges"
        '
        'ImportEdgesMenu
        '
        Me.ImportEdgesMenu.Name = "ImportEdgesMenu"
        Me.ImportEdgesMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.ImportEdgesMenu.Size = New System.Drawing.Size(215, 22)
        Me.ImportEdgesMenu.Text = "&Import Edges"
        '
        'FileMainMenuSeparator2
        '
        Me.FileMainMenuSeparator2.Name = "FileMainMenuSeparator2"
        Me.FileMainMenuSeparator2.Size = New System.Drawing.Size(212, 6)
        '
        'CloseProgramMenu
        '
        Me.CloseProgramMenu.Name = "CloseProgramMenu"
        Me.CloseProgramMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.CloseProgramMenu.Size = New System.Drawing.Size(215, 22)
        Me.CloseProgramMenu.Text = "&Close Program"
        '
        'EdgesMainMenu
        '
        Me.EdgesMainMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteAllMenu, Me.EdgesMainMenuSeparator1, Me.ExtractFromImageMenu, Me.EdgesMainMenuSeparator2, Me.PasteSourceImageMenu, Me.EdgesMainMenuSeparator3, Me.SortBySimilarityMenu, Me.EdgesMainMenuSeparator4, Me.SelectionSubMenu})
        Me.EdgesMainMenu.Name = "EdgesMainMenu"
        Me.EdgesMainMenu.Size = New System.Drawing.Size(50, 20)
        Me.EdgesMainMenu.Text = "Ed&ges"
        '
        'DeleteAllMenu
        '
        Me.DeleteAllMenu.Name = "DeleteAllMenu"
        Me.DeleteAllMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.DeleteAllMenu.Size = New System.Drawing.Size(250, 22)
        Me.DeleteAllMenu.Text = "&Delete All"
        '
        'EdgesMainMenuSeparator1
        '
        Me.EdgesMainMenuSeparator1.Name = "EdgesMainMenuSeparator1"
        Me.EdgesMainMenuSeparator1.Size = New System.Drawing.Size(247, 6)
        '
        'ExtractFromImageMenu
        '
        Me.ExtractFromImageMenu.Name = "ExtractFromImageMenu"
        Me.ExtractFromImageMenu.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.ExtractFromImageMenu.Size = New System.Drawing.Size(250, 22)
        Me.ExtractFromImageMenu.Text = "&Extract From Image"
        '
        'EdgesMainMenuSeparator2
        '
        Me.EdgesMainMenuSeparator2.Name = "EdgesMainMenuSeparator2"
        Me.EdgesMainMenuSeparator2.Size = New System.Drawing.Size(247, 6)
        '
        'PasteSourceImageMenu
        '
        Me.PasteSourceImageMenu.Name = "PasteSourceImageMenu"
        Me.PasteSourceImageMenu.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteSourceImageMenu.Size = New System.Drawing.Size(250, 22)
        Me.PasteSourceImageMenu.Text = "&Paste Source Image"
        '
        'EdgesMainMenuSeparator3
        '
        Me.EdgesMainMenuSeparator3.Name = "EdgesMainMenuSeparator3"
        Me.EdgesMainMenuSeparator3.Size = New System.Drawing.Size(247, 6)
        '
        'SortBySimilarityMenu
        '
        Me.SortBySimilarityMenu.Name = "SortBySimilarityMenu"
        Me.SortBySimilarityMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SortBySimilarityMenu.Size = New System.Drawing.Size(250, 22)
        Me.SortBySimilarityMenu.Text = "&Sort By Similarity"
        '
        'EdgesMainMenuSeparator4
        '
        Me.EdgesMainMenuSeparator4.Name = "EdgesMainMenuSeparator4"
        Me.EdgesMainMenuSeparator4.Size = New System.Drawing.Size(247, 6)
        '
        'SelectionSubMenu
        '
        Me.SelectionSubMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CompareMenu, Me.SelectionSubMenuSeparator1, Me.CopyMenu, Me.CutMenu, Me.PasteMenu, Me.SelectionSubMenuSeparator2, Me.DeleteMenu, Me.MirrorHorizontallyMenu, Me.MirrorVerticallyMenu, Me.RotateOnSideMenu, Me.MoveMenu})
        Me.SelectionSubMenu.Name = "SelectionSubMenu"
        Me.SelectionSubMenu.Size = New System.Drawing.Size(250, 22)
        Me.SelectionSubMenu.Text = "&Selection"
        '
        'CompareMenu
        '
        Me.CompareMenu.Name = "CompareMenu"
        Me.CompareMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.CompareMenu.Size = New System.Drawing.Size(193, 22)
        Me.CompareMenu.Text = "&Compare"
        '
        'SelectionSubMenuSeparator1
        '
        Me.SelectionSubMenuSeparator1.Name = "SelectionSubMenuSeparator1"
        Me.SelectionSubMenuSeparator1.Size = New System.Drawing.Size(190, 6)
        '
        'CopyMenu
        '
        Me.CopyMenu.Name = "CopyMenu"
        Me.CopyMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyMenu.Size = New System.Drawing.Size(193, 22)
        Me.CopyMenu.Text = "&Copy"
        '
        'CutMenu
        '
        Me.CutMenu.Name = "CutMenu"
        Me.CutMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CutMenu.Size = New System.Drawing.Size(193, 22)
        Me.CutMenu.Text = "&Cut"
        '
        'PasteMenu
        '
        Me.PasteMenu.Name = "PasteMenu"
        Me.PasteMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteMenu.Size = New System.Drawing.Size(193, 22)
        Me.PasteMenu.Text = "&Paste"
        '
        'SelectionSubMenuSeparator2
        '
        Me.SelectionSubMenuSeparator2.Name = "SelectionSubMenuSeparator2"
        Me.SelectionSubMenuSeparator2.Size = New System.Drawing.Size(190, 6)
        '
        'DeleteMenu
        '
        Me.DeleteMenu.Name = "DeleteMenu"
        Me.DeleteMenu.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.DeleteMenu.Size = New System.Drawing.Size(193, 22)
        Me.DeleteMenu.Text = "&Delete"
        '
        'MirrorHorizontallyMenu
        '
        Me.MirrorHorizontallyMenu.Name = "MirrorHorizontallyMenu"
        Me.MirrorHorizontallyMenu.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.MirrorHorizontallyMenu.Size = New System.Drawing.Size(193, 22)
        Me.MirrorHorizontallyMenu.Text = "&Mirror Horizontally"
        '
        'MirrorVerticallyMenu
        '
        Me.MirrorVerticallyMenu.Name = "MirrorVerticallyMenu"
        Me.MirrorVerticallyMenu.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.MirrorVerticallyMenu.Size = New System.Drawing.Size(193, 22)
        Me.MirrorVerticallyMenu.Text = "&Mirror Vertically"
        '
        'RotateOnSideMenu
        '
        Me.RotateOnSideMenu.Name = "RotateOnSideMenu"
        Me.RotateOnSideMenu.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.RotateOnSideMenu.Size = New System.Drawing.Size(193, 22)
        Me.RotateOnSideMenu.Text = "&Rotate on Side"
        '
        'MoveMenu
        '
        Me.MoveMenu.Name = "MoveMenu"
        Me.MoveMenu.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.MoveMenu.Size = New System.Drawing.Size(193, 22)
        Me.MoveMenu.Text = "&Move"
        '
        'ViewMainMenu
        '
        Me.ViewMainMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllEdgesMenu, Me.EdgeSourceImageMenu})
        Me.ViewMainMenu.Name = "ViewMainMenu"
        Me.ViewMainMenu.Size = New System.Drawing.Size(44, 20)
        Me.ViewMainMenu.Text = "&View"
        '
        'AllEdgesMenu
        '
        Me.AllEdgesMenu.CheckOnClick = True
        Me.AllEdgesMenu.Name = "AllEdgesMenu"
        Me.AllEdgesMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AllEdgesMenu.Size = New System.Drawing.Size(211, 22)
        Me.AllEdgesMenu.Text = "&All Edges"
        '
        'EdgeSourceImageMenu
        '
        Me.EdgeSourceImageMenu.CheckOnClick = True
        Me.EdgeSourceImageMenu.Name = "EdgeSourceImageMenu"
        Me.EdgeSourceImageMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.EdgeSourceImageMenu.Size = New System.Drawing.Size(211, 22)
        Me.EdgeSourceImageMenu.Text = "Edge &Source Image"
        '
        'OptionsMainMenu
        '
        Me.OptionsMainMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EdgeHighlightColorMenu, Me.EdgePointDisplaySizeMenu, Me.EdgesDisplayColorMenu, Me.OptionsMainMenuSeparator1, Me.ColorMatchModeMenu, Me.ColorToleranceMenu, Me.MinimumEdgeLengthMenu, Me.OptionsMainMenuSeparator2, Me.EdgeCompareModeMEnu})
        Me.OptionsMainMenu.Name = "OptionsMainMenu"
        Me.OptionsMainMenu.Size = New System.Drawing.Size(61, 20)
        Me.OptionsMainMenu.Text = "&Options"
        '
        'EdgeHighlightColorMenu
        '
        Me.EdgeHighlightColorMenu.Name = "EdgeHighlightColorMenu"
        Me.EdgeHighlightColorMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.EdgeHighlightColorMenu.Size = New System.Drawing.Size(232, 22)
        Me.EdgeHighlightColorMenu.Text = "Edge &Highlight Color"
        '
        'EdgePointDisplaySizeMenu
        '
        Me.EdgePointDisplaySizeMenu.Name = "EdgePointDisplaySizeMenu"
        Me.EdgePointDisplaySizeMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.EdgePointDisplaySizeMenu.Size = New System.Drawing.Size(232, 22)
        Me.EdgePointDisplaySizeMenu.Text = "Edge Point Display &Size"
        '
        'EdgesDisplayColorMenu
        '
        Me.EdgesDisplayColorMenu.Name = "EdgesDisplayColorMenu"
        Me.EdgesDisplayColorMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.EdgesDisplayColorMenu.Size = New System.Drawing.Size(232, 22)
        Me.EdgesDisplayColorMenu.Text = "Edges Display &Color"
        '
        'OptionsMainMenuSeparator1
        '
        Me.OptionsMainMenuSeparator1.Name = "OptionsMainMenuSeparator1"
        Me.OptionsMainMenuSeparator1.Size = New System.Drawing.Size(229, 6)
        '
        'ColorMatchModeMenu
        '
        Me.ColorMatchModeMenu.Name = "ColorMatchModeMenu"
        Me.ColorMatchModeMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.ColorMatchModeMenu.Size = New System.Drawing.Size(232, 22)
        Me.ColorMatchModeMenu.Text = "Color &Match Mode"
        '
        'ColorToleranceMenu
        '
        Me.ColorToleranceMenu.Name = "ColorToleranceMenu"
        Me.ColorToleranceMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.ColorToleranceMenu.Size = New System.Drawing.Size(232, 22)
        Me.ColorToleranceMenu.Text = "Color &Tolerance Menu"
        '
        'MinimumEdgeLengthMenu
        '
        Me.MinimumEdgeLengthMenu.Name = "MinimumEdgeLengthMenu"
        Me.MinimumEdgeLengthMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.MinimumEdgeLengthMenu.Size = New System.Drawing.Size(232, 22)
        Me.MinimumEdgeLengthMenu.Text = "&Minimum Edge Length"
        '
        'OptionsMainMenuSeparator2
        '
        Me.OptionsMainMenuSeparator2.Name = "OptionsMainMenuSeparator2"
        Me.OptionsMainMenuSeparator2.Size = New System.Drawing.Size(229, 6)
        '
        'EdgeCompareModeMEnu
        '
        Me.EdgeCompareModeMEnu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IgnoreSizeMenu, Me.RelativeCoordinatesMenu})
        Me.EdgeCompareModeMEnu.Name = "EdgeCompareModeMEnu"
        Me.EdgeCompareModeMEnu.Size = New System.Drawing.Size(232, 22)
        Me.EdgeCompareModeMEnu.Text = "Edge &Compare Mode"
        Me.EdgeCompareModeMEnu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IgnoreSizeMenu
        '
        Me.IgnoreSizeMenu.CheckOnClick = True
        Me.IgnoreSizeMenu.Name = "IgnoreSizeMenu"
        Me.IgnoreSizeMenu.Size = New System.Drawing.Size(182, 22)
        Me.IgnoreSizeMenu.Text = "&Ignore Size"
        '
        'RelativeCoordinatesMenu
        '
        Me.RelativeCoordinatesMenu.CheckOnClick = True
        Me.RelativeCoordinatesMenu.Name = "RelativeCoordinatesMenu"
        Me.RelativeCoordinatesMenu.Size = New System.Drawing.Size(182, 22)
        Me.RelativeCoordinatesMenu.Text = "&Relative Coordinates"
        '
        'HelpMainMenu
        '
        Me.HelpMainMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InformationMenu, Me.HelpMainMenuSeparator1, Me.HelpMenu})
        Me.HelpMainMenu.Name = "HelpMainMenu"
        Me.HelpMainMenu.Size = New System.Drawing.Size(44, 20)
        Me.HelpMainMenu.Text = "&Help"
        '
        'InformationMenu
        '
        Me.InformationMenu.Name = "InformationMenu"
        Me.InformationMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.InformationMenu.Size = New System.Drawing.Size(174, 22)
        Me.InformationMenu.Text = "&Information"
        '
        'HelpMainMenuSeparator1
        '
        Me.HelpMainMenuSeparator1.Name = "HelpMainMenuSeparator1"
        Me.HelpMainMenuSeparator1.Size = New System.Drawing.Size(171, 6)
        '
        'HelpMenu
        '
        Me.HelpMenu.Name = "HelpMenu"
        Me.HelpMenu.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.HelpMenu.Size = New System.Drawing.Size(174, 22)
        Me.HelpMenu.Text = "&Help"
        '
        'MainInterfaceWindow
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 254)
        Me.Controls.Add(Me.ImagePanel)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.MenuBar)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuBar
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "MainInterfaceWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.ImageBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusBar.ResumeLayout(False)
        Me.StatusBar.PerformLayout()
        Me.ImagePanel.ResumeLayout(False)
        Me.MenuBar.ResumeLayout(False)
        Me.MenuBar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusBar As System.Windows.Forms.StatusStrip
   Friend WithEvents ImagePanel As System.Windows.Forms.Panel
   Friend WithEvents ImageBox As System.Windows.Forms.PictureBox
   Friend WithEvents EdgeStatusLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents MenuBar As System.Windows.Forms.MenuStrip
   Friend WithEvents FileMainMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents HelpMainMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgesDisplayColorDialog As System.Windows.Forms.ColorDialog
   Friend WithEvents LoadSourceImageMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents CloseProgramMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents FileMainMenuSeparator1 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents ExportEdgesMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents ImportEdgesMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents FileMainMenuSeparator2 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents ViewMainMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgeSourceImageMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents OptionsMainMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgesDisplayColorMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents ColorMatchModeMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents MinimumEdgeLengthMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents ColorToleranceMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgePointDisplaySizeMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgesMainMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents ExtractFromImageMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents AllEdgesMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgeHighlightColorMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgeHighlightColorDialog As System.Windows.Forms.ColorDialog
   Friend WithEvents DeleteAllMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgesMainMenuSeparator1 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents EdgesMainMenuSeparator2 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents InformationMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents HelpMainMenuSeparator1 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents HelpMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents SelectionSubMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents DeleteMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents MirrorHorizontallyMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents MoveMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents MirrorVerticallyMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgesMainMenuSeparator3 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents PasteSourceImageMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents OptionsMainMenuSeparator1 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents OptionsMainMenuSeparator2 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents EdgeCompareModeMEnu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents RelativeCoordinatesMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents IgnoreSizeMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents CompareMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents SelectionSubMenuSeparator1 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents SortBySimilarityMenu As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents EdgesMainMenuSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectionSubMenuSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RotateOnSideMenu As System.Windows.Forms.ToolStripMenuItem
End Class
