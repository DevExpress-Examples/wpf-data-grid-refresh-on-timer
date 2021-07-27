using System;
using System.Collections;
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
            storageCopy = new ArrayList(storage);
        }

        void OnTick(object sender, EventArgs eventArgs) {
            lock (storage) {
                storageCopy = new ArrayList(storage);
            }
            ListChanged?.Invoke(storage, new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        DispatcherTimer timer;

        private IList storage;

        private ArrayList storageCopy;

        public bool SupportsChangeNotification => true;

        public event ListChangedEventHandler ListChanged;

        public IEnumerator GetEnumerator() {
            return storageCopy.GetEnumerator();
        }

        public object this[int index] { get => storageCopy[index]; set => storageCopy[index] = value; }

        public int Count => storageCopy.Count;

        public bool AllowNew => false;

        public bool AllowEdit => false;

        public bool AllowRemove => false;

        public bool SupportsSearching => false;

        public bool SupportsSorting => false;

        public bool IsReadOnly => storage.IsReadOnly;

        public bool IsFixedSize => storage.IsFixedSize;

        #region NotSupported

        public bool IsSorted => throw new NotSupportedException();

        public PropertyDescriptor SortProperty => throw new NotSupportedException();

        public ListSortDirection SortDirection => throw new NotSupportedException();

        public object SyncRoot => throw new NotSupportedException();

        public bool IsSynchronized => throw new NotSupportedException();

        public object AddNew() {
            throw new NotSupportedException();
        }

        public void AddIndex(PropertyDescriptor property) {
            throw new NotSupportedException();
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction) {
            throw new NotSupportedException();
        }

        public int Find(PropertyDescriptor property, object key) {
            throw new NotSupportedException();
        }

        public void RemoveIndex(PropertyDescriptor property) {
            throw new NotSupportedException();
        }

        public void RemoveSort() {
            throw new NotSupportedException();
        }

        public int Add(object value) {
            throw new NotSupportedException();
        }

        public bool Contains(object value) {
            throw new NotSupportedException();
        }

        public void Clear() {
            throw new NotSupportedException();
        }

        public int IndexOf(object value) {
            throw new NotSupportedException();
        }

        public void Insert(int index, object value) {
            throw new NotSupportedException();
        }

        public void Remove(object value) {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index) {
            throw new NotSupportedException();
        }

        public void CopyTo(Array array, int index) {
            throw new NotSupportedException();
        }
        #endregion
    }
}
