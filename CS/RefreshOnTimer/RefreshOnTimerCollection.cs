using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;


namespace WpfApp6 {
    public class RefreshOnTimerCollection : IBindingList {
        public RefreshOnTimerCollection(TimeSpan interval, IList dataSource) {
            timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = interval;
            timer.Tick += OnTick;
            timer.Start();

            storage = dataSource;
            storageCopy = new List<object>(storage.Cast<object>());
        }

        void OnTick(object sender, EventArgs eventArgs) {
            lock(storage.SyncRoot) {
                storageCopy = new List<object>(storage.Cast<object>());
            }
            ListChanged?.Invoke(storage, new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        DispatcherTimer timer;

        IList storage;

        List<object> storageCopy;

        public bool SupportsChangeNotification => true;

        public event ListChangedEventHandler ListChanged;

        public IEnumerator GetEnumerator() {
            return storageCopy.GetEnumerator();
        }

        public object this[int index] { get => storageCopy[index]; set => new NotSupportedException(); }

        public int Count => storageCopy.Count;

        public bool AllowNew => false;

        public bool AllowEdit => false;

        public bool AllowRemove => false;

        public bool SupportsSearching => false;

        public bool SupportsSorting => false;

        public bool IsReadOnly => storage.IsReadOnly;

        public bool IsFixedSize => storage.IsFixedSize;

        #region NotSupported

        bool IBindingList.IsSorted => throw new NotSupportedException();

        PropertyDescriptor IBindingList.SortProperty => throw new NotSupportedException();

        ListSortDirection IBindingList.SortDirection => throw new NotSupportedException();

        object ICollection.SyncRoot => throw new NotSupportedException();

        bool ICollection.IsSynchronized => throw new NotSupportedException();

        object IBindingList.AddNew() {
            throw new NotSupportedException();
        }

        void IBindingList.AddIndex(PropertyDescriptor property) {
            throw new NotSupportedException();
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction) {
            throw new NotSupportedException();
        }

        int IBindingList.Find(PropertyDescriptor property, object key) {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveIndex(PropertyDescriptor property) {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveSort() {
            throw new NotSupportedException();
        }

        int IList.Add(object value) {
            throw new NotSupportedException();
        }

        bool IList.Contains(object value) {
            throw new NotSupportedException();
        }

        void IList.Clear() {
            throw new NotSupportedException();
        }

        int IList.IndexOf(object value) {
            throw new NotSupportedException();
        }

        void IList.Insert(int index, object value) {
            throw new NotSupportedException();
        }

        void IList.Remove(object value) {
            throw new NotSupportedException();
        }

        void IList.RemoveAt(int index) {
            throw new NotSupportedException();
        }

        void ICollection.CopyTo(Array array, int index) {
            throw new NotSupportedException();
        }
        #endregion
    }
}
