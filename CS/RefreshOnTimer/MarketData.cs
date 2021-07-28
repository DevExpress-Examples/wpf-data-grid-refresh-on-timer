using DevExpress.Mvvm;
using System;

namespace WpfApp6 {
    public class MarketData : BindableBase {
        readonly static Random rnd = new Random();
        const double Max = 950;
        const double Min = 350;

        public MarketData(string name) {
            Ticker = name;
            Open = NextRnd() * (Max - Min) + Min;
            DayVal = Open;
            UpdateInternal(Open);
        }
        public string Ticker { get; private set; }
        public double Last { get; private set; }
        public double ChgPercent { get; private set; }
        public double Open { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double DayVal { get; private set; }

        public void Update() {
            double value = DayVal - (Max - Min) * 0.05 + NextRnd() * (Max - Min) * 0.1;
            if(value <= Min)
                value = Min;
            if(value >= Max)
                value = Max;
            UpdateInternal(value);
        }
        void UpdateInternal(double value) {
            Last = DayVal;
            DayVal = value;
            ChgPercent = (DayVal - Last) * 100.0 / DayVal;
            High = Math.Max(Open, Math.Max(DayVal, Last));
            Low = Math.Min(Open, Math.Min(DayVal, Last));
            RaisePropertyChanged(null);
        }
        static double NextRnd() {
            double value = 0;
            for(int i = 0; i < 5; i++)
                value += rnd.NextDouble();
            return value / 5;
        }
    }
}
