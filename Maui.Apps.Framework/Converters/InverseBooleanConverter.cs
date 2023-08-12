using System.Globalization;

namespace Maui.Apps.Framework.Converters
{
    //Позволяет инвертировать значения типа bool,
    //что может быть полезно при работе с привязками данных
    //в WPF или других подобных технологиях.
    public class InverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Принимает входное значение value, тип данного значения targetType, 
        /// опциональный параметр и информацию о культуре. В данном случае, 
        /// он просто инвертирует значение типа bool, возвращая !((bool)value), 
        /// то есть преобразует true в false и наоборот.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        !((bool)value);

        /// <summary>
        /// Принимает входное значение value, тип данного значения targetType, 
        /// опциональный параметр и информацию о культуре. В данном случае, 
        /// он просто инвертирует значение типа bool, возвращая !((bool)value), 
        /// то есть преобразует true в false и наоборот.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            !((bool)value);
    }
}
