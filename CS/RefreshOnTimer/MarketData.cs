using DevExpress.Mvvm;
using System;

namespace WpfApp6 {
    public class MarketData : BindableBase {
        readonly static Random random = new Random();
        const double Max = 950;
        const double Min = 350;

        public string Ticker { get; private set; }
        public double Last { get; private set; }
        public double ChgPercent { get; private set; }
        public double Open { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double DayVal { get; private set; }

        public MarketData(string name) {
            Ticker = name;
            Open = NextRandom() * (Max - Min) + Min;
            DayVal = Open;
            UpdateInternalCore(Open);
        }

        public void Update() {
            double value = DayVal - (Max - Min) * 0.05 + NextRandom() * (Max - Min) * 0.1;
            if(value <= Min)
                value = Min;
            if(value >= Max)
                value = Max;
            UpdateInternalCore(value);
        }
        void UpdateInternalCore(double value) {
            Last = DayVal;
            DayVal = value;
            ChgPercent = (DayVal - Last) * 100.0 / DayVal;
            High = Math.Max(Open, Math.Max(DayVal, Last));
            Low = Math.Min(Open, Math.Min(DayVal, Last));
            RaisePropertyChanged(null);
        }
        static double NextRandom() {
            double value = 0;
            for(int i = 0; i < 5; i++)
                value += random.NextDouble();
            return value / 5;
        }
    }
}
