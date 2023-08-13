namespace YoutubePlayer.Services
{
    public class YoutubeService : RestServiceBase, IApiService
    {
        public YoutubeService(IConnectivity connectivity, IBarrel cacheBarrel) : base(connectivity, cacheBarrel)
        {
            // устанавливается базовый URL
            SetBaseURL(Constants.ApiServiceURL);
        }

        /// <summary>
        /// Содержит параметры запроса, включая API ключ, закодированную строку поиска и, 
        /// если есть, токен следующей страницы.
        /// Затем, с помощью метода GetAsync класса RestServiceBase, 
        /// отправляется GET-запрос на YouTube API с использованием сформированного 
        /// URL resourceUri и числа 4 (количество попыток повторной попытки при ошибке).
        /// </summary>
        public async Task<VideoSearchResult> SearchVideos(string searchQuery, string nextPageToken = "")
        {
            var resourceUri = $"search?part=snippet&maxResults=10&type=video&key={Constants.ApiKey}&q={WebUtility.UrlEncode(searchQuery)}"
            +
            (!string.IsNullOrEmpty(nextPageToken) ? $"&pageToken={nextPageToken}" : "");

            var result = await GetAsync<VideoSearchResult>(resourceUri, 4);

            return result;
        }
    }
}
