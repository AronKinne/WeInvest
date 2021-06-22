namespace WeInvest.WPF.Controls.Charts.Data {
    public abstract class ChartData<TKey, TValue> : IChartData {

        public TKey Key { get; set; }
        object IChartData.Key {
            get => Key; 
            set => Key = (TKey)value;
        }
        public TValue Value { get; set; }
        object IChartData.Value { 
            get => Value; 
            set => Value = (TValue)value; 
        }

        public ChartData() { }

        public ChartData(TKey key, TValue value) {
            this.Key = key;
            this.Value = value;
        }

        public override string ToString() {
            return $"{{Key = { Key }, Value = { Value }}}";
        }
    }
}
