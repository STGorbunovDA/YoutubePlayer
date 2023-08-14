namespace YoutubePlayer.ViewModels.Base
{
    /// <summary>
    /// Расширяем класс ViewModelBase для навигации в приложении
    /// </summary>
    public partial class AppViewModelBase : ViewModelBase
    {
        public INavigation NavigationService { get; set; }
        public Page PageService { get; set; }

        protected IApiService _appApiService { get; set; }

        /// <summary>
        ///  позволяет инъектировать зависимость от IApiService во все классы, наследующие AppViewModelBase.
        /// </summary>
        /// <param name="appApiService"></param>
        public AppViewModelBase(IApiService appApiService) : base()
        {
            _appApiService = appApiService;
        }

        /// <summary>
        /// возврат назад на предыдущую страницу
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task NavigateBack() =>
            await NavigationService.PopAsync();

        /// <summary>
        /// Закрытие текущего модального окна.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task CloseModal() =>
            await NavigationService.PopModalAsync();

    }
}
