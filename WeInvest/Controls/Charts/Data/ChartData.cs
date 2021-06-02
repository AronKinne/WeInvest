using System;

namespace WeInvest.Controls.Charts.Data {
    public abstract class ChartData<TKey, TValue> {

        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public ChartData() { }

        public ChartData(TKey key, TValue value) {
            this.Key = key;
            this.Value = value;
        }

        public override string ToString() {
            return $"{{Key = { Key }, Value = { Value }}}";
        }

        public static implicit operator ChartData<TKey, TValue>(OrderedLineData v) {
            throw new NotImplementedException();
        }
    }
}
