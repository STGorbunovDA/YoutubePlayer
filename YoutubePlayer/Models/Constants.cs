namespace YoutubePlayer.Models
{
    public static class Constants
    {
        public static string ApplicationName = "YouTubePlayer";
        public static string EmailAddress = @"bous07@mail.ru";
        public static string ApplicationId = "GDA.YouTubePlayer.App"; //"XGENO.TubePlayer.App";
        public static string ApiServiceURL = @"https://youtube.googleapis.com/youtube/v3/";
        public static string ApiKey = @"AIzaSyAuULBprJ_RJljoK1v5keXBEomAseowTio"; //@"AIzaSyAT7UDu2LYMDWHPP09sD7Rq6G-lUSX4CA8";

        //Время анимации
        public static uint MicroDuration { get; set; } = 100;
        public static uint SmallDuration { get; set; } = 300;
        public static uint MediumDuration { get; set; } = 600;
        public static uint LongDuration { get; set; } = 1200;
        public static uint ExtraLongDuration { get; set; } = 1800;
    }
}
