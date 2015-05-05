namespace GitterClient.Converters
{
    using System;

    using Windows.UI.Xaml.Data;

    /// <summary>
    /// The time converter.
    /// </summary>
    public class TimeConverter : IValueConverter
    {
        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>
        /// The <see cref="object" />.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var formatedTime = value.ToString().Split('T');
            return string.Format("{0} : {1}", formatedTime[0], formatedTime[1].Substring(0, 5));
        }

        /// <summary>
        /// The convert back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>
        /// The <see cref="object" />.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
