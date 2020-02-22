using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace WPFStudentInteraction.Converters {
    public class BooleanConverter<T> : IValueConverter {
        public T True { get; set; }

        public T False { get; set; }

        public BooleanConverter() { }

        public BooleanConverter(T trueValue, T falseValue) {
            True = trueValue;
            False = falseValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value is bool boolValue && boolValue ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value is T castValue && EqualityComparer<T>.Default.Equals(castValue, True);
        }
    }
}