using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6 {
    public class DataItem : INotifyPropertyChanged {

        private string name;
        public string Name { 
            get => name;
            set {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            } 
        }

        private int id;
        public int Id {
            get => id; 
            set {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
