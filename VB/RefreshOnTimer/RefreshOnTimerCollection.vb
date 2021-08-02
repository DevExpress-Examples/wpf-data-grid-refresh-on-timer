Imports System
Imports System.Linq
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

'INSTANT VB NOTE: The field listChanged was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private listChanged_Conflict As ListChangedEventHandler

		Private Custom Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged
			AddHandler(ByVal value As ListChangedEventHandler)
				listChanged_Conflict = DirectCast(System.Delegate.Combine(listChanged_Conflict, DirectCast(value, ListChangedEventHandler)), ListChangedEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As ListChangedEventHandler)
				listChanged_Conflict = DirectCast(System.Delegate.Remove(listChanged_Conflict, DirectCast(value, ListChangedEventHandler)), ListChangedEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
				If listChanged_Conflict IsNot Nothing Then
					listChanged_Conflict.Invoke(sender, e)
				End If
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
			For Each item In storage
				storageCopy.Add(item)
			Next item
		End Sub

		Private Sub OnTick(ByVal sender As Object, ByVal eventArgs As EventArgs)
			SyncLock storage.SyncRoot
				CopyStorage()
			End SyncLock
			listChanged_Conflict?.Invoke(storage, New ListChangedEventArgs(ListChangedType.Reset, 0))
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

		Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return storageCopy.GetEnumerator()
		End Function

		Private ReadOnly Property IBindingList_SupportsChangeNotification() As Boolean Implements IBindingList.SupportsChangeNotification
			Get
				Return True
			End Get
		End Property

		Public Property IList_Item(ByVal index As Integer) As Object Implements IList.Item
			Get
				Return storageCopy(index)
			End Get
			Set(ByVal value As Object)
				Dim tempVar As New NotSupportedException()
			End Set
		End Property

		Private ReadOnly Property ICollection_Count() As Integer Implements ICollection.Count
			Get
				Return storageCopy.Count
			End Get
		End Property

		Private ReadOnly Property IBindingList_AllowNew() As Boolean Implements IBindingList.AllowNew
			Get
				Return False
			End Get
		End Property

		Private ReadOnly Property IBindingList_AllowEdit() As Boolean Implements IBindingList.AllowEdit
			Get
				Return False
			End Get
		End Property

		Private ReadOnly Property IBindingList_AllowRemove() As Boolean Implements IBindingList.AllowRemove
			Get
				Return False
			End Get
		End Property

		Private ReadOnly Property IBindingList_SupportsSearching() As Boolean Implements IBindingList.SupportsSearching
			Get
				Return False
			End Get
		End Property

		Private ReadOnly Property IBindingList_SupportsSorting() As Boolean Implements IBindingList.SupportsSorting
			Get
				Return False
			End Get
		End Property

		Private ReadOnly Property IList_IsReadOnly() As Boolean Implements IList.IsReadOnly
			Get
				Return storage.IsReadOnly
			End Get
		End Property

		Private ReadOnly Property IList_IsFixedSize() As Boolean Implements IList.IsFixedSize
			Get
				Return storage.IsFixedSize
			End Get
		End Property

		Private ReadOnly Property IBindingList_IsSorted() As Boolean Implements IBindingList.IsSorted
			Get
'INSTANT VB TODO TASK: Throw expressions are not converted by Instant VB:
'ORIGINAL LINE: return throw new NotSupportedException();
				Return throw New NotSupportedException()
			End Get
		End Property

		Private ReadOnly Property IBindingList_SortProperty() As PropertyDescriptor Implements IBindingList.SortProperty
			Get
'INSTANT VB TODO TASK: Throw expressions are not converted by Instant VB:
'ORIGINAL LINE: return throw new NotSupportedException();
				Return throw New NotSupportedException()
			End Get
		End Property

		Private ReadOnly Property IBindingList_SortDirection() As ListSortDirection Implements IBindingList.SortDirection
			Get
'INSTANT VB TODO TASK: Throw expressions are not converted by Instant VB:
'ORIGINAL LINE: return throw new NotSupportedException();
				Return throw New NotSupportedException()
			End Get
		End Property

		Private ReadOnly Property ICollection_SyncRoot() As Object Implements ICollection.SyncRoot
			Get
'INSTANT VB TODO TASK: Throw expressions are not converted by Instant VB:
'ORIGINAL LINE: return throw new NotSupportedException();
				Return throw New NotSupportedException()
			End Get
		End Property

		Private ReadOnly Property ICollection_IsSynchronized() As Boolean Implements ICollection.IsSynchronized
			Get
'INSTANT VB TODO TASK: Throw expressions are not converted by Instant VB:
'ORIGINAL LINE: return throw new NotSupportedException();
				Return throw New NotSupportedException()
			End Get
		End Property

		Private Function IBindingList_AddNew() As Object Implements IBindingList.AddNew
			Throw New NotSupportedException()
		End Function

		Private Sub IBindingList_AddIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.AddIndex
			Throw New NotSupportedException()
		End Sub

		Private Sub IBindingList_ApplySort(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection) Implements IBindingList.ApplySort
			Throw New NotSupportedException()
		End Sub

		Private Function IBindingList_Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
			Throw New NotSupportedException()
		End Function

		Private Sub IBindingList_RemoveIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.RemoveIndex
			Throw New NotSupportedException()
		End Sub

		Private Sub IBindingList_RemoveSort() Implements IBindingList.RemoveSort
			Throw New NotSupportedException()
		End Sub

		Private Function IList_Add(ByVal value As Object) As Integer Implements IList.Add
			Throw New NotSupportedException()
		End Function

		Private Function IList_Contains(ByVal value As Object) As Boolean Implements IList.Contains
			Throw New NotSupportedException()
		End Function

		Private Sub IList_Clear() Implements IList.Clear
			Throw New NotSupportedException()
		End Sub

		Private Function IList_IndexOf(ByVal value As Object) As Integer Implements IList.IndexOf
			Throw New NotSupportedException()
		End Function

		Private Sub IList_Insert(ByVal index As Integer, ByVal value As Object) Implements IList.Insert
			Throw New NotSupportedException()
		End Sub

		Private Sub IList_Remove(ByVal value As Object) Implements IList.Remove
			Throw New NotSupportedException()
		End Sub

		Private Sub IList_RemoveAt(ByVal index As Integer) Implements IList.RemoveAt
			Throw New NotSupportedException()
		End Sub

		Private Sub ICollection_CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
			Throw New NotSupportedException()
		End Sub
	End Class
End Namespace