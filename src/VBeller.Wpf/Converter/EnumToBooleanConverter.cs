using System;
using System.Windows;
using System.Windows.Data;

namespace VBeller.Wpf.Converter
{
    /// <summary>
    /// Provides conversion from <see cref="Enum"/> to <see cref="bool"/>, if the value equals the expected value.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="Enum"/> value to <see cref="bool"/> by comparing the value to the expected one passed as <see cref="parameter"/>.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> value to compare to the expected one.</param>
        /// <param name="targetType">Ignored.</param>
        /// <param name="parameter">The expted <see cref="Enum"/> value.</param>
        /// <param name="culture">Ignored.</param>
        /// <returns>True, if <see cref="value"/> equals <see cref="parameter"/>, otherwise false.
        /// <see cref="DependencyProperty.UnsetValue"/> if parameters are missing.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (value == null || !Enum.IsDefined(value.GetType(), value))
                return DependencyProperty.UnsetValue;

            var parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        /// <summary>
        /// Converts the <see cref="parameter"/> to the corresponding <see cref="targetType"/> enum value.
        /// </summary>
        /// <param name="value">Ignored.</param>
        /// <param name="targetType">The target <see cref="Enum"/> type.</param>
        /// <param name="parameter">The string representaton of the <see cref="Enum"/> value.</param>
        /// <param name="culture">Ignored.</param>
        /// <returns>The <see cref="targetType"/> enum value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var parameterString = parameter as string;
            return parameterString == null ? DependencyProperty.UnsetValue : Enum.Parse(targetType, parameterString);
        }
    }
}