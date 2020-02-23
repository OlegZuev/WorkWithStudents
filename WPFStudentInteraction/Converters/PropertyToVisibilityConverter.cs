using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Data;

namespace WPFStudentInteraction.Converters {
    public class PropertyToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter == null) {
                throw new ArgumentException("PropertyToVisibilityConverter should have parameter");
            }

            return value != null && value.GetType().GetProperties().Any(p => p.Name == (string) parameter)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}