Imports System
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Threading

Namespace RefreshOnTimer

    Public Class RefreshOnTimerCollection
        Inherits IBindingList
        Implements IDisposable

        Private timer As DispatcherTimer

        Private storage As IList

        Private storageCopy As List(Of Object)

        Private disposed As Boolean

        Private listChanged As ListChangedEventHandler

         ''' Cannot convert EventDeclarationSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitEventDeclaration(EventDeclarationSyntax node) in D:\DXVCS\CSharpToVB\CSharpToVB\CodeConverter-master\CodeConverter\VB\NodesVisitor.cs:line 823
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping) in D:\DXVCS\CSharpToVB\CSharpToVB\CodeConverter-master\CodeConverter\VB\CommentConvertingVisitorWrapper.cs:line 26
''' 
''' Input:
''' 
'''         event ListChangedEventHandler IBindingList.ListChanged {
'''             add {
'''                 this.listChanged += value;
'''             }
'''             remove {
'''                 this.listChanged -= value;
'''             }
'''         }
''' 
'''  Public Sub New(ByVal interval As TimeSpan, ByVal dataSource As IList)
            timer = New DispatcherTimer(DispatcherPriority.Background)
            timer.Interval = interval
            Me.timer.Tick += AddressOf OnTick
            timer.Start()
            storage = dataSource
            CopyStorage()
        End Sub

        Private Sub CopyStorage()
            storageCopy = New List(Of Object)(storage.Count)
            For Each item In storage
                storageCopy.Add(item)
            Next
        End Sub

        Private Sub OnTick(ByVal sender As Object, ByVal eventArgs As EventArgs)
            SyncLock storage.SyncRoot
                CopyStorage()
            End SyncLock

            listChanged?.Invoke(storage, New ListChangedEventArgs(ListChangedType.Reset, 0))
        End Sub

        Public Sub Dispose()
            Me.Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not disposed Then
                If disposing Then
                    timer.[Stop]()
                End If

                disposed = True
            End If
        End Sub

        Private Function GetEnumerator() As IEnumerator
            Return storageCopy.GetEnumerator()
        End Function

        Private ReadOnly Property SupportsChangeNotification As Boolean
            Get
                Return True
            End Get
        End Property

        Private Property Item(ByVal index As Integer) As Object
            Get
                Return storageCopy(index)
            End Get

            Set(ByVal value As Object)
                New NotSupportedException()
            End Set
        End Property

        Private ReadOnly Property Count As Integer
            Get
                Return storageCopy.Count
            End Get
        End Property

        Private ReadOnly Property AllowNew As Boolean
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property AllowEdit As Boolean
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property AllowRemove As Boolean
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property SupportsSearching As Boolean
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property SupportsSorting As Boolean
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property IsReadOnly As Boolean
            Get
                Return storage.IsReadOnly
            End Get
        End Property

        Private ReadOnly Property IsFixedSize As Boolean
            Get
                Return storage.IsFixedSize
            End Get
        End Property

        Private ReadOnly Property IsSorted As Boolean
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private ReadOnly Property SortProperty As PropertyDescriptor
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private ReadOnly Property SortDirection As ListSortDirection
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private ReadOnly Property SyncRoot As Object
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private ReadOnly Property IsSynchronized As Boolean
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private Function AddNew() As Object
            Throw New NotSupportedException()
        End Function

        Private Sub AddIndex(ByVal [property] As PropertyDescriptor)
            Throw New NotSupportedException()
        End Sub

        Private Sub ApplySort(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection)
            Throw New NotSupportedException()
        End Sub

        Private Function Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer
            Throw New NotSupportedException()
        End Function

        Private Sub RemoveIndex(ByVal [property] As PropertyDescriptor)
            Throw New NotSupportedException()
        End Sub

        Private Sub RemoveSort()
            Throw New NotSupportedException()
        End Sub

        Private Function Add(ByVal value As Object) As Integer
            Throw New NotSupportedException()
        End Function

        Private Function Contains(ByVal value As Object) As Boolean
            Throw New NotSupportedException()
        End Function

        Private Sub Clear()
            Throw New NotSupportedException()
        End Sub

        Private Function IndexOf(ByVal value As Object) As Integer
            Throw New NotSupportedException()
        End Function

        Private Sub Insert(ByVal index As Integer, ByVal value As Object)
            Throw New NotSupportedException()
        End Sub

        Private Sub Remove(ByVal value As Object)
            Throw New NotSupportedException()
        End Sub

        Private Sub RemoveAt(ByVal index As Integer)
            Throw New NotSupportedException()
        End Sub

        Private Sub CopyTo(ByVal array As Array, ByVal index As Integer)
            Throw New NotSupportedException()
        End Sub
    End Class
End Namespace
