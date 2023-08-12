namespace Maui.Apps.Framework.Converters
{
    //Класс нужен для корзины покупателя
    public class EnumerationContainsElementConverter : IValueConverter
    {
        /// <summary>
        /// Метод Convert является реализацией метода Convert из интерфейса IValueConverter. 
        /// Он принимает входное значение value, тип targetType, параметр и информацию о культуре culture.
        /// Внутри метода, значение value приводится к типу IEnumerable<object>.
        /// Затем вызывается метод Count() на приведенном значении и результат сравнивается с 0. 
        /// Если Count() больше 0, то метод вернет true, иначе - false.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        (value as IEnumerable<object>)?.Count() > 0;

        /// <summary>
        /// Яляется заглушкой, который всегда выбрасывает исключение NotImplementedException.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
