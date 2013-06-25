using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CineworldPortable.Entities;
using Newtonsoft.Json;

namespace CineworldPortable
{
    public class CineworldClient
    {
        #region Private constants

        private const string BaseUrl = "http://www.cineworld.com/api/";

        #endregion

        #region Constructors
        public CineworldClient(HttpClientHandler handler)
            : this(handler, string.Empty)
        {
        }

        public CineworldClient(HttpClientHandler handler, string apiKey)
        {
            HttpClient = handler == null
                             ? new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip })
                             : new HttpClient(handler);
            ApiKey = apiKey;
        }

        public CineworldClient(string apiKey)
            : this(null, apiKey)
        {
        }

        public CineworldClient()
            : this(null, string.Empty)
        {
        }
        #endregion

        #region Public Properties
        public HttpClient HttpClient { get; private set; }

        public string ApiKey { get; set; }
        #endregion

        #region Public methods
        public async Task<List<Cinema>> GetCinemasAsync(
            Territory? territory = null,
            bool fullDetails = false,
            List<int> filmEdis = null,
            List<string> dates = null,
            int? cinemaId = null,
            string categoryCode = null,
            string eventCode = null,
            int? distributorId = null)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            AddParameters(territory, fullDetails, filmEdis, dates, cinemaId, categoryCode, eventCode, distributorId, null, data);

            var response = await GetDataAsync<CinemaResponse>(data, "quickbook/cinemas");

            return response != null && response.Cinemas != null
                       ? response.Cinemas.ToList()
                       : new List<Cinema>();
        }

        private static void AddParameters(Territory? territory, bool fullDetails, List<int> filmEdis, List<string> dates, int? cinemaId, string categoryCode, string eventCode, int? distributorId, List<int> cinemaIds, Dictionary<string, string> data)
        {
            if (territory.HasValue)
            {
                data.Add("terriroty", territory.Value.GetDescription());
            }

            if (fullDetails)
            {
                data.Add("full", "true");
            }

            if (filmEdis != null && filmEdis.Any())
            {
                if (filmEdis.Count == 1)
                {
                    data.Add("film", filmEdis[0].ToString());
                }
                else
                {
                    var s = filmEdis[0].ToString();
                    filmEdis.RemoveAt(0);
                    foreach (var edi in filmEdis)
                    {
                        s += string.Format("&film={0}", edi);
                    }

                    data.Add("film", s);
                }
            }

            if (dates != null && dates.Any())
            {
                if (dates.Count == 1)
                {
                    data.Add("date", dates[0]);
                }
                else
                {
                    var s = dates[0];
                    dates.RemoveAt(0);
                    foreach (var date in dates)
                    {
                        s += string.Format("&date={0}", date);
                    }

                    data.Add("date", s);
                }
            }

            if (cinemaId.HasValue)
            {
                data.Add("cinema", cinemaId.Value.ToString());
            }

            if (!string.IsNullOrEmpty(categoryCode))
            {
                data.Add("category", categoryCode);
            }

            if (!string.IsNullOrEmpty(eventCode))
            {
                data.Add("event", eventCode);
            }

            if (distributorId.HasValue)
            {
                data.Add("distributor", distributorId.Value.ToString());
            }
        }

        public async Task<List<Film>> GetFilmsAsync(
            Territory? territory = null,
            bool fullDetails = false,
            List<string> dates = null,
            List<int> cinemaIds = null,
            string categoryCode = null,
            string eventCode = null,
            int? distributorId = null)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            AddParameters(territory, fullDetails, null, dates, null, categoryCode, eventCode, distributorId, data);

            var response = await GetDataAsync<FilmsResponse>(data, "quickbook/films");

            return response != null && response.Films != null
                       ? response.Films.ToList()
                       : new List<Film>();
        }

        public async Task<List<string>> GetDatesAsync()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            var response = await GetDataAsync<DatesResponse>(data, "quickbook/dates");

            return response != null && response.Dates != null
                       ? response.Dates.ToList()
                       : new List<string>();
        }

        public async Task<List<Performance>> GetPerformancesAsync()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            var response = await GetDataAsync<PerformancesResponse>(data, "quickbook/performances");

            return response != null && response.Performances != null
                       ? response.Performances.ToList()
                       : new List<Performance>();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            var response = await GetDataAsync<CategoriesResponse>(data, "quickbook/categories");

            return response != null && response.Categories != null
                       ? response.Categories.ToList()
                       : new List<Category>();
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            var response = await GetDataAsync<EventsResponse>(data, "quickbook/events");

            return response != null && response.Events != null
                       ? response.Events.ToList()
                       : new List<Event>();
        }

        public async Task<List<Distributor>> GetDistributorsAsync()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            var response = await GetDataAsync<DistributorsResponse>(data, "distributors");

            return response != null && response.Distributors != null
                       ? response.Distributors.ToList()
                       : new List<Distributor>();
        } 
        #endregion

        #region Private methods
        private static string GetCineworldUrl(string methodCall, Dictionary<string, string> queryData)
        {
            return string.Format("{0}{1}{2}", BaseUrl, methodCall, queryData.ToUrlParams());
        }

        private Dictionary<string, string> CreateBaseDictionary()
        {
            var result = new Dictionary<string, string> {{"key", ApiKey}};

            return result;
        } 

        private async Task<T> GetDataAsync<T>(Dictionary<string, string> queryData, string methodCall)
        {
            var url = GetCineworldUrl(methodCall, queryData);

            var response = await HttpClient.GetStringAsync(url);

            var responseItem = JsonConvert.DeserializeObject<T>(response);

            return responseItem;
        }
        #endregion
    }
}
