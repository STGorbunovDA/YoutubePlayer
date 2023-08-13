namespace YoutubePlayer.Models
{
    //[JsonPropertyName], указывает на соответствующее поле
    //в JSON-строке при десериализации.В результате, эти классы могут
    //быть использованы для десериализации JSON-ответов от YouTube API
    //в объекты.NET.


    /// <summary>
    ///  содержит информацию о следующей странице (NextPageToken), информацию о странице (PageInfo) и список видео (Items).
    /// </summary>
    public class VideoSearchResult
    {
        [JsonPropertyName("nextPageToken")] 
        public string NextPageToken { get; set; }

        [JsonPropertyName("pageInfo")]
        public PageInfo PageInfo { get; set; }

        [JsonPropertyName("items")]
        public List<YoutubeVideo> Items { get; set; }
    }

    /// <summary>
    /// содержит информацию о общем количестве результатов (TotalResults) и количестве результатов на странице (ResultsPerPage).
    /// </summary>
    public class PageInfo
    {
        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }

        [JsonPropertyName("resultsPerPage")]
        public int ResultsPerPage { get; set; }
    }

    /// <summary>
    ///  представляет отдельное видео на YouTube и содержит информацию о его идентификаторе (Id) и описании (Snippet).
    /// </summary>
    public class YoutubeVideo
    {
        [JsonPropertyName("id")]
        public Id Id { get; set; }

        [JsonPropertyName("snippet")]
        public Snippet Snippet { get; set; }
    }

    /// <summary>
    /// содержит идентификатор видео (VideoId).
    /// </summary>
    public class Id
    {
        [JsonPropertyName("videoId")]
        public string VideoId { get; set; }
    }

    /// <summary>
    /// содержит информацию о дате публикации (PublishedAt), 
    /// идентификаторе канала (ChannelId), заголовке (Title), описании (Description), 
    /// разрешении видео (Thumbnails) и заголовке канала (ChannelTitle).
    /// </summary>
    public class Snippet
    {
        [JsonPropertyName("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("channelId")]
        public string ChannelId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("thumbnails")]
        public Thumbnails Thumbnails { get; set; }

        [JsonPropertyName("channelTitle")]
        public string ChannelTitle { get; set; }
    }

    /// <summary>
    /// содержит информацию о разрешении видео (Medium и High).
    /// </summary>
    public class Thumbnails
    {
        [JsonPropertyName("medium")]
        public Thumbnail Medium { get; set; }

        [JsonPropertyName("high")]
        public Thumbnail High { get; set; }
    }

    /// <summary>
    /// содержит URL разрешения видео (Url).
    /// </summary>
    public class Thumbnail
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
