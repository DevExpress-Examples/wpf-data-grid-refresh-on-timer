using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;

namespace WpfApp6 {
    public class RefreshOnTimerCollection : IBindingList, IDisposable {
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
            listChanged?.Invoke(storage, new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        DispatcherTimer timer;

        IList storage;

        List<object> storageCopy;

        bool disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(!disposed) {
                if(disposing) {
                    timer.Stop();
                }

                disposed = true;
            }
        }

        private ListChangedEventHandler listChanged;

        event ListChangedEventHandler IBindingList.ListChanged {
            add {
                listChanged += value;
            }
            remove {
                listChanged -= value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return storageCopy.GetEnumerator();
        }

        bool IBindingList.SupportsChangeNotification => true;

        object IList.this[int index] { get => storageCopy[index]; set => new NotSupportedException(); }

        int ICollection.Count => storageCopy.Count;

        bool IBindingList.AllowNew => false;

        bool IBindingList.AllowEdit => false;

        bool IBindingList.AllowRemove => false;

        bool IBindingList.SupportsSearching => false;

        bool IBindingList.SupportsSorting => false;

        bool IList.IsReadOnly => storage.IsReadOnly;

        bool IList.IsFixedSize => storage.IsFixedSize;

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
    }
}