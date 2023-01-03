Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Threading

Namespace RefreshOnTimer

    Public Class RefreshOnTimerCollection
        Implements IBindingList, IDisposable

        Private timer As DispatcherTimer

        Private storage As IList

        Private storageCopy As List(Of Object)

        Private disposed As Boolean

        Private listChangedField As ListChangedEventHandler

        Private Custom Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged
            AddHandler(ByVal value As ListChangedEventHandler)
                listChangedField = [Delegate].Combine(listChangedField, value)
            End AddHandler

            RemoveHandler(ByVal value As ListChangedEventHandler)
                listChangedField = [Delegate].Remove(listChangedField, value)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As ListChangedEventArgs)
                listChangedField ?(sender, e)
            End RaiseEvent
        End Event

        Public Sub New(ByVal interval As TimeSpan, ByVal dataSource As IList)
            timer = New DispatcherTimer(DispatcherPriority.Background)
            timer.Interval = interval
            AddHandler timer.Tick, AddressOf OnTick
            timer.Start()
            storage = dataSource
            CopyStorage()
        End Sub

        Private Sub CopyStorage()
            storageCopy = New List(Of Object)(storage.Count)
            For Each element In storage
                storageCopy.Add(element)
            Next
        End Sub

        Private Sub OnTick(ByVal sender As Object, ByVal eventArgs As EventArgs)
            SyncLock storage.SyncRoot
                CopyStorage()
            End SyncLock

            listChangedField?.Invoke(storage, New ListChangedEventArgs(ListChangedType.Reset, 0))
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not disposed Then
                If disposing Then
                    timer.Stop()
                End If

                disposed = True
            End If
        End Sub

        Private Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return storageCopy.GetEnumerator()
        End Function

        Private ReadOnly Property SupportsChangeNotification As Boolean Implements IBindingList.SupportsChangeNotification
            Get
                Return True
            End Get
        End Property

        Private Property Item(ByVal index As Integer) As Object Implements IList.Item
            Get
                Return storageCopy(index)
            End Get

            Set(ByVal value As Object)
                Throw New NotSupportedException()
            End Set
        End Property

        Private ReadOnly Property Count As Integer Implements ICollection.Count
            Get
                Return storageCopy.Count
            End Get
        End Property

        Private ReadOnly Property AllowNew As Boolean Implements IBindingList.AllowNew
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property AllowEdit As Boolean Implements IBindingList.AllowEdit
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property AllowRemove As Boolean Implements IBindingList.AllowRemove
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property SupportsSearching As Boolean Implements IBindingList.SupportsSearching
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property SupportsSorting As Boolean Implements IBindingList.SupportsSorting
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property IsReadOnly As Boolean Implements IList.IsReadOnly
            Get
                Return storage.IsReadOnly
            End Get
        End Property

        Private ReadOnly Property IsFixedSize As Boolean Implements IList.IsFixedSize
            Get
                Return storage.IsFixedSize
            End Get
        End Property

        Private ReadOnly Property IsSorted As Boolean Implements IBindingList.IsSorted
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private ReadOnly Property SortProperty As PropertyDescriptor Implements IBindingList.SortProperty
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private ReadOnly Property SortDirection As ListSortDirection Implements IBindingList.SortDirection
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        Private Function AddNew() As Object Implements IBindingList.AddNew
            Throw New NotSupportedException()
        End Function

        Private Sub AddIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.AddIndex
            Throw New NotSupportedException()
        End Sub

        Private Sub ApplySort(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection) Implements IBindingList.ApplySort
            Throw New NotSupportedException()
        End Sub

        Private Function Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
            Throw New NotSupportedException()
        End Function

        Private Sub RemoveIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.RemoveIndex
            Throw New NotSupportedException()
        End Sub

        Private Sub RemoveSort() Implements IBindingList.RemoveSort
            Throw New NotSupportedException()
        End Sub

        Private Function Add(ByVal value As Object) As Integer Implements IList.Add
            Throw New NotSupportedException()
        End Function

        Private Function Contains(ByVal value As Object) As Boolean Implements IList.Contains
            Throw New NotSupportedException()
        End Function

        Private Sub Clear() Implements IList.Clear
            Throw New NotSupportedException()
        End Sub

        Private Function IndexOf(ByVal value As Object) As Integer Implements IList.IndexOf
            Throw New NotSupportedException()
        End Function

        Private Sub Insert(ByVal index As Integer, ByVal value As Object) Implements IList.Insert
            Throw New NotSupportedException()
        End Sub

        Private Sub Remove(ByVal value As Object) Implements IList.Remove
            Throw New NotSupportedException()
        End Sub

        Private Sub RemoveAt(ByVal index As Integer) Implements IList.RemoveAt
            Throw New NotSupportedException()
        End Sub

        Private Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
            Throw New NotSupportedException()
        End Sub
    End Class
End Namespace
