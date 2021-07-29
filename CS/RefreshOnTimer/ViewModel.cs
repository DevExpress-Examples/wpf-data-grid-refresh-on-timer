using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace WpfApp6 {
    class ViewModel : ViewModelBase {
        #region Stocks
        static readonly string[] Names = new[] {
            "ANR", "FE", "GT", "PRGO", "APD", "PPL", "AES", "AVB", "IBM", "GAS", "EFX", "GPC", "ICE", "IVZ", "KO",
            "CCE", "SO", "STI", "BWA", "HRL", "WFM", "LM", "TROW", "K", "EXPE", "PCAR", "TRIP", "WHR", "WMT", "NU",
            "HST", "CVH", "LMT", "MAR", "CVC", "RF", "VMC", "PHM", "MU", "IRM", "AMT", "BXP", "STT", "PBCT", "FISV",
            "BLL", "MTB", "DIS", "LH", "AKAM", "CPB", "MYL", "LIFE", "LEG", "SCG", "CNX", "COL", "MCHP", "GR", "DUK",
            "BAC", "NUE", "UNM", "DLTR", "ABC", "TEG", "RRD", "EQR", "EXC", "BA", "CME", "NTRS", "VTR", "FITB", "PG",
            "KR", "M", "SNI", "ETN", "CLF", "PH", "KEY", "SHW", "HD", "AFL", "TSS", "CMI", "HBAN", "AEP", "BIG", "LTD",
            "ESRX", "GLW", "WPI", "MON", "AAPL", "DF", "T", "CMA", "THC", "LUV", "TXN", "TIE", "PX",
        };

        static readonly string[] AdditionalNames = new[] {
            "ZM", "RE", "BSX", "PPD", "LB", "OLN", "ENPH", "NVKR", "GNRC"
        };
        #endregion

        Timer timer1;
        Timer timer2;
        Timer timer3;

        Random random = new Random();

        Stack<MarketData> additionalData;

        object syncRoot;
        ObservableCollection<MarketData> data;

        public RefreshOnTimerCollection Source { get; set; }

        public ViewModel() {
            data = new ObservableCollection<MarketData>(Names.Select(name => new MarketData(name)).ToList());
            syncRoot = ((ICollection)data).SyncRoot;
            additionalData = new Stack<MarketData>(AdditionalNames.Select(name => new MarketData(name)));
            Source = new RefreshOnTimerCollection(TimeSpan.FromSeconds(1), data);
            timer1 = new Timer(UpdateRows, null, 0, 1);
            timer2 = new Timer(TryAddNewRow, null, 10, 1);
            timer3 = new Timer(TryRemoveRow, null, 20, 1);
        }

        [Command]
        public void DisposeViewModel() {
            timer1.Dispose();
            timer2.Dispose();
            timer3.Dispose();
            Source.Dispose();
        }

        void UpdateRows(object state) {
            lock(syncRoot) {
                if(random.Next() % 2 == 0 && additionalData.Count > 0) {
                    data.Add(additionalData.Pop());
                }
            }
        }

        void TryAddNewRow(object state) {
            lock(syncRoot) {
                if(random.Next() % 2 == 0 && additionalData.Count < AdditionalNames.Length) {
                    var dataItem = data.First(x => AdditionalNames.Contains(x.Ticker));
                    data.Remove(dataItem);
                    additionalData.Push(dataItem);
                }
            }
        }

        void TryRemoveRow(object state) {
            lock(syncRoot) {
                for(int i = 0; i < 2; i++) {
                    int row = random.Next(0, data.Count);
                    data[row].Update();
                }
            }
        }
    }
}
