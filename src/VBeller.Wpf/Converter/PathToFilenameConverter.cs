using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace VBeller.Wpf.Converter
{
    /// <summary>
    /// Provides conversion from full file path to file name only.
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    internal class PathToFilenameConverter : IValueConverter
    {
        /// <summary>
        /// Converts a full file path to it's file name.
        /// To remove the file extension, pass true as the parameter.
        /// </summary>
        /// <param name="value">The full file path.</param>
        /// <param name="targetType">Ignored.</param>
        /// <param name="parameter">A <see cref="bool"/> indicating, if the file extension should be removed.</param>
        /// <param name="culture">Ignored.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string filePath)
            {
                return parameter is bool removeExt && removeExt
                    ? Path.GetFileNameWithoutExtension(filePath)
                    : Path.GetFileName(filePath);
            }
            return null;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="value">Ignored.</param>
        /// <param name="targetType">Ignored.</param>
        /// <param name="parameter">Ignored.</param>
        /// <param name="culture">Ignored.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Gets always thrown.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{typeof(PathToFilenameConverter)} can only be used one way.");
        }
    }
}