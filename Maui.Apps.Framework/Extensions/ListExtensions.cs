using System.Collections.ObjectModel;

namespace Maui.Apps.Framework.Extensions
{
    //В данном коде определен статический класс "ListExtensions",
    //который содержит несколько расширяющих методов для типа "ObservableCollection<T>" и "IEnumerable<T>".
    public static class ListExtensions
    {
        /// <summary>
        /// Расширяет функциональность "ObservableCollection<T>". 
        /// Он позволяет добавить коллекцию новых элементов "newItems"в конец исходной коллекции.
        /// Если параметр "clearFirst" установлен в значение true, то перед добавлением новых элементов 
        /// исходная коллекция будет очищена.
        /// </summary>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> newItems, bool clearFirst = false)
        {
            if (clearFirst)
                collection.Clear();

            newItems.ForEach(newItem => collection.Add(newItem));
        }

        /// <summary>
        /// Метод "Randomize<T>" расширяет функциональность "IEnumerable<T>".
        /// Он возвращает случайно перемешанную версию исходной коллекции "source".
        /// Для этого метод использует объект "Random" для генерации случайных чисел
        /// и сортирует исходную коллекцию по случайному значению.
        /// </summary>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }

        /// <summary>
        /// Метод "ForEach<T>" также расширяет функциональность "IEnumerable<T>".
        /// Он выполняет указанное действие "action" для каждого элемента коллекции "enumeration"
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }
    }
}
