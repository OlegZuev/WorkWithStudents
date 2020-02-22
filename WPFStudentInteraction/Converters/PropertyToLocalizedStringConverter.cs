using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Data;

namespace WPFStudentInteraction.Converters {
    public class PropertyToLocalizedStringConverter : IValueConverter {
        private readonly ResourceSet _localResourceSet =
            Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, false);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var castValue = value as PropertyInfo;
            return castValue != null ? _localResourceSet.GetString(castValue.Name) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}