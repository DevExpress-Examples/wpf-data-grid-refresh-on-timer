using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections.Specialized;
using DevExpress.Data.Helpers;
using DevExpress.Data.Filtering.Helpers;


namespace WpfApp6 {
    public class RefreshOnTimerCollection : IEnumerable, INotifyCollectionChanged {
        protected static IBindingList GetListSource(object source, bool prioritizeIListSource) {
            if(source is IListSource && (prioritizeIListSource || !(source is IEnumerable)))
                return ExtractListSource(((IListSource)source).GetList());
            return ExtractListSource(source as IEnumerable);
        }
        static IBindingList ExtractListSource(IEnumerable source) {
            if(source == null)
                return null;
            var list = source as IList;
            if(list == null) {
                list = new EnumerableObservableWrapper<object>((IEnumerable)source);
            }
            if(list is IBindingList) {
                return (IBindingList)list;
            }
            return BindingListAdapterBase.CreateFromList(list, ItemPropertyNotificationMode.PropertyChanged);
        }

        public RefreshOnTimerCollection(TimeSpan interval, object dataSource) {
            timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = interval;
            timer.Tick += OnTick;
            timer.Start();

            storage = GetListSource(dataSource, false);
            storage.ListChanged += Storage_ListChanged;
        }

        private void Storage_ListChanged(object sender, ListChangedEventArgs e) {
            changed = true;
        }

        bool changed;

        void OnTick(object sender, EventArgs eventArgs) {
            if(changed) {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            changed = false;
        }

        DispatcherTimer timer;

        private IBindingList storage;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            CollectionChanged?.Invoke(this, e);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        IEnumerator IEnumerable.GetEnumerator() {
            return storage.GetEnumerator();
        }
    }
}
