namespace YoutubePlayer.Views.Base
{
    /// <summary>
    /// Предоставляет базовую реализацию страницы, 
    /// которая обеспечивает инициализацию и связывание модели представления с контекстом страницы
    /// </summary>
    public class ViewBase<TViewModel> : PageBase where TViewModel : AppViewModelBase
    {
        // Для отслеживания загрузки страницы.
        protected bool _isLoaded = false;

        // Представляет модель представления страницы.
        protected TViewModel ViewModel { get; set; }

        // Содержит параметры инициализации модели представления.
        protected object ViewModelParameters { get; set; }

        // Возникает при инициализации модели представления.
        protected event EventHandler ViewModelInitialized;

        // Вызывает конструктор базового класса
        public ViewBase() : base()
        {
        }

        // Вызывает конструктор базового класса и принимает параметры инициализации модели представления
        public ViewBase(object initParameters) : base() =>
            ViewModelParameters = initParameters;

        // Переопределен для выполнения инициализации
        // модели представления при отображении страницы.
        protected override void OnAppearing()
        {
            //Initialize only if page is not loaded previously
            if (!_isLoaded)
            {
                base.OnAppearing(); // Вызывается метод базового класса OnAppearing()

                // Устанавливается связывание данных (BindingContext) между страницей и моделью представления.
                // Инициализируется ViewModel путем получения сервиса с помощью ServiceHelper.GetService<TViewModel>().
                BindingContext = ViewModel = ServiceHelper.GetService<TViewModel>();

                // Назначается текущая навигация и страница модели представления.
                ViewModel.NavigationService = this.Navigation;
                ViewModel.PageService = this;

                // Событие и обработчик. Вызывает событие ViewModelInitialized
                // и передает текущий объект и аргумент типа EventArgs
                // для обработки события в соответствующем обработчике.
                ViewModelInitialized?.Invoke(this, new EventArgs());

                //Вызывается метод OnNavigatedTo() модели представления с передачей параметров инициализации.
                ViewModel.OnNavigatedTo(ViewModelParameters);

                // Страница загружена.
                _isLoaded = true;
            }
        }
    }
}
