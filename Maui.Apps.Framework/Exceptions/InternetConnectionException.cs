namespace Maui.Apps.Framework.Exceptions
{
    //В данном коде определен пользовательский класс исключения с названием "InternetConnectionException",
    //который наследуется от базового класса "Exception".
    //Данный класс предоставляет возможность создания пользовательского исключения
    //"InternetConnectionException" с указанием дополнительного сообщения об ошибке
    //и/или передачей внутреннего исключения.
    public class InternetConnectionException : Exception
    {
        /// <summary> Конструктор без параметров, который вызывает конструктор базового класса без параметров. </summary>
        public InternetConnectionException()
        {
        }

        /// <summary>
        /// Конструктор с параметром "string message",
        ///который вызывает конструктор базового класса с параметром "string message".
        ///Он позволяет указать сообщение об ошибке при создании исключения.
        /// </summary>
        public InternetConnectionException(string message) : base(message)
        {
        }


        /// <summary>
        /// Конструктор с параметрами "string message" и "Exception inner",
        ///который вызывает конструктор базового класса с параметрами "string message"
        ///и "Exception inner". Он позволяет указать сообщение об ошибке и исключение, вызвавшее данное исключение.
        /// </summary>
        public InternetConnectionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
