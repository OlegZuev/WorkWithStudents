using System.Windows;

namespace WPFStudentInteraction.Converters {
    public class BooleanToVisibilityConverter : BooleanConverter<Visibility> {
        public BooleanToVisibilityConverter() { }

        public BooleanToVisibilityConverter(Visibility trueValue, Visibility falseValue) : base(trueValue, falseValue) { }
    }
}