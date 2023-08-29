namespace YoutubePlayer.ViewModels
{
    public class StartPageViewModel : AppViewModelBase
    {
        public StartPageViewModel(IApiService appApiService) : base(appApiService)
        {
            this.Title = "YOUTUBE PLAYER";
        }
        public override async void OnNavigatedTo(object parameters)
        {
            SetDataLodingIndicators(true);

            LoadingText = "Идёт загрузка";
            //await Search();
            try 
            {
                await Task.Delay(3000);
                throw new Exception("Unable to reach Google Youtube API Service");
                throw new InternetConnectionException();
                this.DataLoaded = true;
            }
            catch (InternetConnectionException iex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Медленное или отсутствующее подключение к Интернету." + Environment.NewLine + "Пожалуйста, проверьте подключение к Интернету и повторите попытку.";
                ErrorImage = "nointernet.png";
            }
            catch (Exception ex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = $"Что-то пошло не так. Если проблема сохраняется, обратитесь в службу поддержки по адресу: {Constants.EmailAddress} с сообщением об ошибке:" + Environment.NewLine + Environment.NewLine + ex.Message;
                ErrorImage = "error.png";
            }
            finally 
            {
                SetDataLodingIndicators(false);
            }
        }

    }
}
