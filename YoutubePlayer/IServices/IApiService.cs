namespace YoutubePlayer.IServices
{
    public interface IApiService
    {
        /// <summary>
        /// Представляет результат поиска видео, хранящий всю необходимую информацию 
        /// о найденных видео, такую как заголовки, длительность, авторы и т. д.
        /// </summary>
        Task<VideoSearchResult> SearchVideos(string searchQuery, string nextPageToken = "");
    }
}
