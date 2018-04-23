' Developer Express Code Central Example:
' How to Create a Custom Column Chooser
' 
' This example shows how to create a custom standalone Column Chooser, and display
' it within LayoutControl.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3991

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core

Namespace DXGrid_CustomColumnChooser.SL
    Partial Public Class MainPage
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = IssueList.GetData()
            InitCustomColumnChooser(tableView)

        End Sub
        Private Sub InitCustomColumnChooser(ByVal view As GridViewBase)
            columnChooser.View = view
            view.ColumnChooserFactory = New CustomColumnChooser(columnChooser)
            view.ShowColumnChooser()
        End Sub
    End Class
    Public Class CustomColumnChooser
        Implements IColumnChooser, IColumnChooserFactory

        Private ReadOnly columnChooserControl As CustomColumnChooserControl
        Public Sub New(ByVal columnChooserControl As CustomColumnChooserControl)
            Me.columnChooserControl = columnChooserControl
        End Sub
        #Region "IColumnChooser Members"
        Private Sub IColumnChooser_Show() Implements IColumnChooser.Show
        End Sub
        Private Sub IColumnChooser_Hide() Implements IColumnChooser.Hide
        End Sub
        Private Sub IColumnChooser_ApplyState(ByVal state As IColumnChooserState) Implements IColumnChooser.ApplyState
        End Sub
        Private Sub IColumnChooser_SaveState(ByVal state As IColumnChooserState) Implements IColumnChooser.SaveState
        End Sub
        Private Sub IColumnChooser_Destroy() Implements IColumnChooser.Destroy
        End Sub
        Private ReadOnly Property IColumnChooser_TopContainer() As UIElement Implements IColumnChooser.TopContainer
            Get
                Return columnChooserControl.ColunmChooserControl
            End Get
        End Property
        #End Region

        #Region "IColumnChooserFactory Members"

        Private Function IColumnChooserFactory_Create(ByVal owner As DevExpress.Xpf.Core.WPFCompatibility.SLControl) As IColumnChooser Implements IColumnChooserFactory.Create
            Return Me
        End Function

        #End Region
    End Class
    Public Class CustomColumnChooserControl
        Inherits Control

        Public Shared ReadOnly ViewProperty As DependencyProperty
        Shared Sub New()
            ViewProperty = DependencyProperty.Register("View", GetType(GridViewBase), GetType(CustomColumnChooserControl), New PropertyMetadata(Nothing))
        End Sub
        Public Sub New()
            DefaultStyleKey = GetType(CustomColumnChooserControl)
        End Sub
        Public Property View() As GridViewBase
            Get
                Return CType(GetValue(ViewProperty), GridViewBase)
            End Get
            Set(ByVal value As GridViewBase)
                SetValue(ViewProperty, value)
            End Set
        End Property
        Private privateColunmChooserControl As ColumnChooserControl
        Friend Property ColunmChooserControl() As ColumnChooserControl
            Get
                Return privateColunmChooserControl
            End Get
            Private Set(ByVal value As ColumnChooserControl)
                privateColunmChooserControl = value
            End Set
        End Property
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            ColunmChooserControl = CType(GetTemplateChild("PART_ColumnChooserControl"), ColumnChooserControl)
        End Sub
    End Class
    Public Class IssueList
        Public Shared Function GetData() As List(Of IssueDataObject)
            Dim data As New List(Of IssueDataObject)()
            data.Add(New IssueDataObject() With {.IssueName = "Transaction History", .IssueType = "Bug", .IsPrivate = True})
            data.Add(New IssueDataObject() With {.IssueName = "Ledger: Inconsistency", .IssueType = "Bug", .IsPrivate = False})
            data.Add(New IssueDataObject() With {.IssueName = "Data Import", .IssueType = "Request", .IsPrivate = False})
            data.Add(New IssueDataObject() With {.IssueName = "Data Archiving", .IssueType = "Request", .IsPrivate = True})
            Return data
        End Function
    End Class

    Public Class IssueDataObject
        Public Property IssueName() As String
        Public Property IssueType() As String
        Public Property IsPrivate() As Boolean
    End Class
End Namespace
