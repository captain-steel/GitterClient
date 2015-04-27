namespace GitterClient.Converters
{
    using System;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// The one to one converter.
    /// </summary>
    public class OneToOneConverter : IValueConverter
    {
        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var @bool = value as bool?;
            if (@bool != null && @bool.Value)
            {
                return new BitmapImage(new Uri("http://business-icon.com/material/096.png"));
            }
            else
            {
                return new BitmapImage(new Uri("http://icons.iconarchive.com/icons/devcom/network/256/globe-Vista-icon.png"));
            }
        }

        /// <summary>
        /// The convert back
        /// </summary>
        /// <param name="value">The name.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>NotImplementedException</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
