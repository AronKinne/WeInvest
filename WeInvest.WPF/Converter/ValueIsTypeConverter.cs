using System;
using System.Globalization;
using System.Windows.Data;

namespace WeInvest.WPF.Converter {
    public class ValueIsTypeConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null || parameter == null)
                return false;

            return Equals(value.GetType().ToString(), parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

    }
}
