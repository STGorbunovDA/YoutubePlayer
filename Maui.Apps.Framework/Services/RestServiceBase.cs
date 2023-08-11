namespace Maui.Apps.Framework.Services
{
    //Этот код представляет базовый класс RestServiceBase для создания REST-сервисов.
    //Класс содержит методы для выполнения HTTP-запросов (GET, POST, PUT, DELETE)
    //и установки основного URL, а также добавления заголовков к запросам.
    //С помощью этого базового класса можно создать конкретные классы сервисов,
    //которые наследуют RestServiceBase и определяют дополнительную функциональность
    //для работы с определенным API. Класс RestServiceBase предоставляет общие методы
    //для выполнения HTTP-запросов и управления подключениями и кэшем данных.
    public class RestServiceBase
    {
        private HttpClient _httpClient;
        private IBarrel _cacheBarrel;
        private IConnectivity _connectivity;


        /// <summary>
        /// Конструктор класса принимает два параметра: IConnectivity и IBarrel.
        /// Эти параметры представляют зависимости, которые будут использоваться
        /// в классе для проверки подключения к интернету и кэширования данных.
        /// </summary>
        protected RestServiceBase(IConnectivity connectivity, IBarrel cacheBarrel)
        {
            _cacheBarrel = cacheBarrel;
            _connectivity = connectivity;
        }

        /// <summary>
        /// Метод SetBaseURL устанавливает базовый URL для HTTP-клиента,
        /// а также устанавливает заголовки accept для принятия только JSON-данных.
        /// </summary>
        protected void SetBaseURL(string apiBaseUrl)
        {
            _httpClient = new()
            {
                BaseAddress = new Uri(apiBaseUrl)
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
        }

        /// <summary>
        /// Метод AddHttpHeader добавляет HTTP-заголовок с указанным ключом
        /// и значением к стандартным заголовкам запроса.
        /// </summary>
        protected void AddHttpHeader(string key, string value) =>
        _httpClient.DefaultRequestHeaders.Add(key, value);

        /// <summary>
        /// Метод GetAsync выполняет GET-запрос по указанному ресурсу 
        /// и возвращает результат в виде объекта типа T. 
        /// Он также поддерживает кэширование данных на заданное количество часов, 
        /// если объект кэша (_cacheBarrel) предоставлен и если данные еще не просрочены.
        /// </summary>
        protected async Task<T> GetAsync<T>(string resource, int cacheDuration = 24)
        {
            //Get Json data (from Cache or Web)
            var json = await GetJsonAsync(resource, cacheDuration);

            //Return the result
            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Данный метод представляет собой асинхронную операцию получения JSON-строки из указанного ресурса.
        /// </summary>
        private async Task<string> GetJsonAsync(string resource, int cacheDuration = 24)
        {
            //Происходит очистка ключа кэша с помощью метода CleanCacheKey,
            //который, предположительно, удаляет неправильные символы из ключа.
            var cleanCacheKey = resource.CleanCacheKey();

            //Затем происходит проверка, включен ли Barrel кэш (библиотека для кэширования данных).
            if (_cacheBarrel is not null) 
            {
                //Если он включен, то выполняется попытка получения закэшированных данных из кэша
                //с использованием метода Get.
                var cachedData = _cacheBarrel.Get<string>(cleanCacheKey);

                // Если данные найдены в кэше и не истек срок их действия
                // (не истекла указанная в cacheDuration продолжительность),
                // то эти данные возвращаются из метода.
                if (cacheDuration > 0 && cachedData is not null && !_cacheBarrel.IsExpired(cleanCacheKey))
                    return cachedData;

                //Если доступ к интернету отсутствует (NetworkAccess не равен NetworkAccess.Internet)
                //и закэшированные данные не найдены, будет выброшено исключение InternetConnectionException.
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    return cachedData is not null ? cachedData : throw new InternetConnectionException();
                }
            }

            //Если доступ к интернету отсутствует (NetworkAccess не равен NetworkAccess.Internet)
            //и закэшированные данные не найдены, будет выброшено исключение InternetConnectionException.
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                throw new InternetConnectionException();

            //Если нет закэшированных данных или доступ к интернету есть,
            //будет отправлен запрос по указанному в resource адресу с использованием HttpClient.
            //Метод GetAsync возвращает ответ в виде HttpResponseMessage.
            var response = await _httpClient.GetAsync(new Uri(_httpClient.BaseAddress, resource));

            //Затем проверяется, что ответ является успешным,
            //используя метод EnsureSuccessStatusCode.
            //Если ответ не успешен (код состояния не в пределах 2xx), будет выброшено исключение.
            response.EnsureSuccessStatusCode();

            //Затем происходит чтение содержимого ответа в виде строки
            //JSON с использованием метода ReadAsStringAsync и возвращается полученная строка.
            string json = await response.Content.ReadAsStringAsync();

            //Если указана продолжительность кэширования (cacheDuration больше 0) и Barrel кэш включен,          
            if (cacheDuration > 0 && _cacheBarrel is not null)
            {
                try
                {
                    //то происходит попытка сохранить полученную JSON-строку в кэше с использованием метода Add.
                    //Продолжительность кэширования задается в виде TimeSpan с использованием TimeSpan.FromHours.
                    _cacheBarrel.Add(cleanCacheKey, json, TimeSpan.FromHours(cacheDuration));
                }
                catch { }
            }

            //возвращается полученная JSON-строка.
            return json;
        }

        //Метод PostAsync выполняет POST-запрос на указанный URI с данными (payload) в формате JSON.
        protected async Task<HttpResponseMessage> PostAsync<T>(string uri, T payload)
        {
            var dataToPost = JsonSerializer.Serialize(payload);
            var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(new Uri(_httpClient.BaseAddress, uri), content);

            response.EnsureSuccessStatusCode();

            return response;
        }

        //Метод PutAsync выполняет PUT-запрос на указанный URI с данными (payload) в формате JSON.
        protected async Task<HttpResponseMessage> PutAsync<T>(string uri, T payload)
        {
            var dataToPost = JsonSerializer.Serialize(payload);
            var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(new Uri(_httpClient.BaseAddress, uri), content);

            response.EnsureSuccessStatusCode();

            return response;
        }

        //Метод DeleteAsync выполняет DELETE-запрос на указанный URI.
        protected async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(new Uri(_httpClient.BaseAddress, uri));

            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
