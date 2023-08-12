namespace YoutubePlayer.Helpers
{
    /// <summary>
    /// Получение провайдера служб
    /// </summary>
    public static class ServiceHelper
    {
        /// <summary>
        /// Возвращает текущего провайдера служб
        /// </summary>
        public static TService GetService<TService>() => //test
        Current.GetService<TService>();

        public static IServiceProvider Current =>
#if WINDOWS10_0_17763_0_OR_GREATER
			MauiWinUIApplication.Current.Services;
#elif ANDROID
    MauiApplication.Current.Services;
#elif IOS || MACCATALYST
                MauiUIApplicationDelegate.Current.Services;
#else
			null;
#endif
    }
}
