using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFStudentInteraction.Converters {
    public class IndexToBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null && (int) value != -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}