

namespace YoutubePlayer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("FiraSans-Light.ttf", "RegularFont");
                    fonts.AddFont("FiraSans-Medium.ttf", "MediumFont");
                })
                  .ConfigureLifecycleEvents(events =>
                  {
#if ANDROID
                      events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));
                      //Метод MakeStatusBarTranslucent получает объект Activity
                      //и использует методы этого объекта для установки флагов
                      //и цвета статусной строки. Конкретные методы, которые вызываются,
                      //позволяют установить флаги окна, чтобы оно заполняло все пространство
                      //на экране, очищает флаги, которые делают статусную строку прозрачной,
                      //и устанавливает прозрачный цвет статусной строки.
                      static void MakeStatusBarTranslucent(Android.App.Activity activity)
                      {
                          activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);

                          activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

                          activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
                      }
#endif
                  });
            //Register Services
            RegisterAppServices(builder.Services);


            return builder.Build();
        }
        /// <summary>
        ///  Регистрация различных сервисов в контейнере внедрения зависимостей.
        /// </summary>
        private static void RegisterAppServices(IServiceCollection services)
        {
            // Происходит регистрация зависимости IConnectivity,
            // которая представляет доступ к информации о подключении к сети.
            // Она зарегистрирована как Singleton, что означает,
            // что будет создан только один экземпляр этой зависимости,
            // который будет использоваться во всем приложении.
            services.AddSingleton<IConnectivity>(Connectivity.Current);

            // Зависимость IBarrel, предоставляет возможность хранить
            // и извлекать данные из кэша приложения. В данном случае,
            // текущий экземпляр IBarrel получается через статический
            // доступ к классу Barrel, а затем регистрируется
            // в контейнере как Singleton.
            Barrel.ApplicationId = Constants.ApplicationId;
            services.AddSingleton<IBarrel>(Barrel.Current);


            // Регистрируется зависимость IApiService,
            // которая представляет сервис для работы с API YouTube.
            // Класс YoutubeService выбран как реализация этого интерфейса.
            // Также зависимость регистрируется как Singleton.
            services.AddSingleton<IApiService, YoutubeService>();

            //Register View Models
            services.AddSingleton<StartPageViewModel>();
        }
    }
}