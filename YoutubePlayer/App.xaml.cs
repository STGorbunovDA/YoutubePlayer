#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace YoutubePlayer
{
    public partial class App : Application
    {
        const int WindowWidth = 540;
        const int WindowHeight = 1000;

        public App()
        {
            InitializeComponent();

            // Метод, который отслеживает и записывает информацию о версии приложения.
            // Позволяет получить информацию о текущей версии приложения для отображения в пользовательском интерфейсе или отправки на сервер.
            // Позволяет отслеживать обновления и сравнивать текущую версию приложения с новыми доступными версиями.
            // Позволяет выполнить необходимые действия при обновлении приложения, такие как выполнение миграций базы данных или обновление локальных файлов.
            VersionTracking.Track();

            // В данном коде происходит добавление обработчика для интерфейса IWindow
            // в маппинг (соответствие) типов для обработки окон в Microsoft.Maui.
            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
#if WINDOWS
            // Получение объектов mauiWindow (виртуальное представление окна в Maui) 
            // и nativeWindow (платформенное представление окна) из обработчика.
            var mauiWindow = handler.VirtualView;            
            var nativeWindow = handler.PlatformView;

            // Активация платформенного окна, чтобы оно стало активным окном операционной системы.
            nativeWindow.Activate();

            // Получение дескриптора окна (windowHandle) с помощью 
            // WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow). 
            // Дескриптор окна используется для управления окном на низком уровне из C# кода.
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);

            // Получение идентификатора окна (windowId) с помощью 
            // Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle). 
            // Идентификатор окна используется для получения объекта AppWindow, 
            // представляющего окно на уровне операционной системы Windows.
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            // Изменение размера AppWindow на заданные значения WindowWidth и WindowHeight 
            // с помощью метода Resize(new SizeInt32(WindowWidth, WindowHeight)).
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));

            // В результате, при вызове этого маппинга для типа IWindow, 
            // произойдет активация и изменение размера платформенного 
            // окна на уровне операционной системы Windows.
#endif
            });

            MainPage = new NavigationPage(new StartPage());
        }
    }
}