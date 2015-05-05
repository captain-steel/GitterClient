namespace GitterClient
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The binding utility.
    /// </summary>
    public class BindingUtility
    {
        /// <summary>
        /// The get update source on change.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool GetUpdateSourceOnChange(DependencyObject d)
        {
            return (bool)d.GetValue(UpdateSourceOnChangeProperty);
        }

        /// <summary>
        /// The set update source on change.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="value">The value.</param>
        public static void SetUpdateSourceOnChange(DependencyObject d, bool value)
        {
            d.SetValue(UpdateSourceOnChangeProperty, value);
        }

        /// <summary>
        /// The update source on change property.
        /// </summary>
        public static readonly DependencyProperty UpdateSourceOnChangeProperty = DependencyProperty.RegisterAttached("UpdateSourceOnChange", typeof(bool), typeof(BindingUtility), new PropertyMetadata(false, OnPropertyChanged));

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox == null)
                return;
            if ((bool)e.NewValue)
            {
                textBox.TextChanged += OnTextChanged;
            }
            else
            {
                textBox.TextChanged -= OnTextChanged;
            }
        }

        /// <summary>
        /// The on text changed.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The e.</param>
        public static void OnTextChanged(object s, TextChangedEventArgs e)
        {
            var textBox = s as TextBox;
            if (textBox == null)
                return;

            var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
            {
                bindingExpression.UpdateSource();
            }
        }
    }
}
