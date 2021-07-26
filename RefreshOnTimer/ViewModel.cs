using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;

namespace WpfApp6 {
    class ViewModel {

        static object SyncRoot = new object();

        Timer timer;

        int counter = 1;

        protected void TimerCallback(object state) {
            lock(SyncRoot) {
                counter++;
                storage.Add(new DataItem() { Id = counter, Name = "A" });
            }
        }

        public ViewModel() {
            storage = new ObservableCollection<DataItem>() { new DataItem() { Id = 1, Name = "A" } };
            timer = new Timer(TimerCallback, null, 1000, 1000);
            Source = new RefreshOnTimerCollection(TimeSpan.FromSeconds(3), storage);
            AddNew = new DelegateCommand(() => {
                counter++;
                storage.Add(new DataItem() { Id = counter, Name = "A" });
            });
            EditFirst = new DelegateCommand(() => {
                storage[0].Name = "Edited";
            });
        }

        /// <summary>
        /// Customer's collection to perform changes
        /// </summary>
        private ObservableCollection<DataItem> storage;

        /// <summary>
        /// Refresh on timer collection to be bound 
        /// </summary>
        public RefreshOnTimerCollection Source { get; set; }

        public DelegateCommand AddNew { get; }

        public DelegateCommand EditFirst { get; }
    }
}
