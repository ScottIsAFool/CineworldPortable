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

        /// <summary>
        ///     The base URL
        /// </summary>
        private const string BaseUrl = "http://www.cineworld.com/api/";

        #endregion

        #region Private properties

        /// <summary>
        ///     The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CineworldClient" /> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public CineworldClient(HttpClientHandler handler)
            : this(handler, string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CineworldClient" /> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="apiKey">The API key.</param>
        public CineworldClient(HttpClientHandler handler, string apiKey)
        {
            _httpClient = handler == null
                ? new HttpClient(new HttpClientHandler {AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip})
                : new HttpClient(handler);
            ApiKey = apiKey;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CineworldClient" /> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        public CineworldClient(string apiKey)
            : this(null, apiKey)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CineworldClient" /> class.
        /// </summary>
        public CineworldClient()
            : this(null, string.Empty)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the API key.
        /// </summary>
        /// <value>
        ///     The API key.
        /// </value>
        public string ApiKey
        {
            get;
            set;
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Gets the cinemas.
        /// </summary>
        /// <param name="territory">The territory. [OPTIONAL]</param>
        /// <param name="fullDetails">if set to <c>true</c> [full details]. [OPTIONAL]</param>
        /// <param name="filmEdis">The film edis (IDs). [OPTIONAL]</param>
        /// <param name="dates">The dates. Each date should be YYYYMMDD in format. [OPTIONAL]</param>
        /// <param name="cinemaId">The cinema id. [OPTIONAL]</param>
        /// <param name="categoryCode">The category code. [OPTIONAL]</param>
        /// <param name="eventCode">The event code. [OPTIONAL]</param>
        /// <param name="distributorId">The distributor id. [OPTIONAL]</param>
        /// <returns>The list of available cinemas</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
        public async Task<List<Cinema>> GetCinemasAsync(
            Territory territory = Territory.UnitedKingdom,
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

            AddParameters(data, territory, fullDetails, filmEdis, dates, cinemaId, categoryCode, eventCode, distributorId);

            var response = await GetDataAsync<CinemaResponse>(data, "quickbook/cinemas");

            return response != null && response.Cinemas != null
                ? response.Cinemas.ToList()
                : new List<Cinema>();
        }

        /// <summary>
        ///     Gets the films.
        /// </summary>
        /// <param name="territory">The territory. [OPTIONAL]</param>
        /// <param name="fullDetails">if set to <c>true</c> [full details]. [OPTIONAL]</param>
        /// <param name="dates">The dates. Each date should be YYYYMMDD in format. [OPTIONAL]</param>
        /// <param name="cinemaIds">The cinema ids. [OPTIONAL]</param>
        /// <param name="categoryCode">The category code. [OPTIONAL]</param>
        /// <param name="eventCode">The event code. [OPTIONAL]</param>
        /// <param name="filmId">The film id. [OPTIONAL]</param>
        /// <param name="distributorId">The distributor id. [OPTIONAL]</param>
        /// <returns>The list of films matching the given criteria</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
        public async Task<List<Film>> GetFilmsAsync(
            Territory territory = Territory.UnitedKingdom,
            bool fullDetails = false,
            List<string> dates = null,
            List<int> cinemaIds = null,
            string categoryCode = null,
            string eventCode = null,
            int? filmId = null,
            int? distributorId = null)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            AddParameters(data, territory, fullDetails, dates: dates, categoryCode: categoryCode, eventCode: eventCode, distributorId: distributorId, cinemaIds: cinemaIds, filmId: filmId);

            var response = await GetDataAsync<FilmsResponse>(data, "quickbook/films");

            return response != null && response.Films != null
                ? response.Films.ToList()
                : new List<Film>();
        }

        /// <summary>
        ///     Gets the dates.
        /// </summary>
        /// <param name="territory">The territory. [OPTIONAL]</param>
        /// <param name="cinemaId">The cinema id. [OPTIONAL]</param>
        /// <param name="filmId">The film id. [OPTIONAL]</param>
        /// <param name="categoryCode">The category code. [OPTIONAL]</param>
        /// <param name="eventCode">The event code. [OPTIONAL]</param>
        /// <param name="distributorId">The distributor id. [OPTIONAL]</param>
        /// <returns>The list of dates for the given criteria</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
        public async Task<List<string>> GetDatesAsync(
            Territory territory = Territory.UnitedKingdom,
            int? cinemaId = null,
            int? filmId = null,
            string categoryCode = null,
            string eventCode = null,
            int? distributorId = null)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            AddParameters(data, territory, cinemaId: cinemaId, filmId: filmId, categoryCode: categoryCode, eventCode: eventCode, distributorId: distributorId);

            var response = await GetDataAsync<DatesResponse>(data, "quickbook/dates");

            return response != null && response.Dates != null
                ? response.Dates.ToList()
                : new List<string>();
        }

        /// <summary>
        ///     Gets the performances.
        /// </summary>
        /// <param name="cinemaId">The cinema id.</param>
        /// <param name="filmId">The film id.</param>
        /// <param name="date">The date. The date should be YYYYMMDD in format.</param>
        /// <param name="territory">The territory. [OPTIONAL]</param>
        /// <param name="campaign">The campaign. [OPTIONAL]</param>
        /// <returns>The list of performances for that film</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">
        ///     cinemaId;Cinema ID cannot be null or empty
        ///     or
        ///     filmId;Film ID cannot be null or empty
        ///     or
        ///     date;Date cannot be null or empty
        /// </exception>
        public async Task<List<Performance>> GetPerformancesAsync(
            int? cinemaId,
            int? filmId,
            string date,
            Territory territory = Territory.UnitedKingdom,
            string campaign = null)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new NullReferenceException("Api Key cannot be null or empty");
            }

            if (!cinemaId.HasValue)
            {
                throw new ArgumentNullException("cinemaId", "Cinema ID cannot be null or empty");
            }

            if (!filmId.HasValue)
            {
                throw new ArgumentNullException("filmId", "Film ID cannot be null or empty");
            }

            if (string.IsNullOrEmpty(date))
            {
                throw new ArgumentNullException("date", "Date cannot be null or empty");
            }

            var data = CreateBaseDictionary();

            AddParameters(data, territory, cinemaId: cinemaId, filmId: filmId, performanceDate: date);

            if (!string.IsNullOrEmpty(campaign))
            {
                data.Add("campaign", campaign);
            }

            var response = await GetDataAsync<PerformancesResponse>(data, "quickbook/performances");

            return response != null && response.Performances != null
                ? response.Performances.ToList()
                : new List<Performance>();
        }

        /// <summary>
        ///     Gets the categories.
        /// </summary>
        /// <returns>The list of film categories</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
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

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <returns>Returns the list of events</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
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

        /// <summary>
        /// Gets the distributors async.
        /// </summary>
        /// <returns>The list of film distributors</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
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

        /// <summary>
        /// Gets the cineworld URL.
        /// </summary>
        /// <param name="methodCall">The method call.</param>
        /// <param name="queryData">The query data.</param>
        /// <returns>The Cineworld API url</returns>
        private static string GetCineworldUrl(string methodCall, Dictionary<string, string> queryData)
        {
            return string.Format("{0}{1}{2}", BaseUrl, methodCall, queryData.ToUrlParams());
        }

        /// <summary>
        /// Adds the parameters for the query string dictionary.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="territory">The territory.</param>
        /// <param name="fullDetails">if set to <c>true</c> [full details].</param>
        /// <param name="filmEdis">The film edis.</param>
        /// <param name="dates">The dates.</param>
        /// <param name="cinemaId">The cinema id.</param>
        /// <param name="categoryCode">The category code.</param>
        /// <param name="eventCode">The event code.</param>
        /// <param name="distributorId">The distributor id.</param>
        /// <param name="cinemaIds">The cinema ids.</param>
        /// <param name="filmId">The film id.</param>
        /// <param name="performanceDate">The performance date.</param>
        private static void AddParameters(
            Dictionary<string, string> data,
            Territory territory = Territory.UnitedKingdom,
            bool fullDetails = false,
            List<int> filmEdis = null,
            List<string> dates = null,
            int? cinemaId = null,
            string categoryCode = null,
            string eventCode = null,
            int? distributorId = null,
            List<int> cinemaIds = null,
            int? filmId = null,
            string performanceDate = null)
        {
            if (territory == Territory.Ireland)
            {
                data.Add("terriroty", territory.GetDescription());
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
                    string s = filmEdis[0].ToString();
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
                    string s = dates[0];
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

            if (cinemaIds != null && cinemaIds.Any())
            {
                if (cinemaIds.Count == 1)
                {
                    data.Add("cinema", cinemaIds[0].ToString());
                }
                else
                {
                    string s = cinemaIds[0].ToString();
                    cinemaIds.RemoveAt(0);
                    foreach (var cinema in cinemaIds)
                    {
                        s += string.Format("&cinema={0}", cinema);
                    }

                    data.Add("cinema", s);
                }
            }

            if (filmId.HasValue)
            {
                data.Add("film", filmId.ToString());
            }

            if (!string.IsNullOrEmpty(performanceDate))
            {
                data.Add("date", performanceDate);
            }
        }

        /// <summary>
        /// Creates the base dictionary.
        /// </summary>
        /// <returns>The base dictionary with the API Key in it</returns>
        private Dictionary<string, string> CreateBaseDictionary()
        {
            var result = new Dictionary<string, string> {{"key", ApiKey}};

            return result;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="queryData">The query data.</param>
        /// <param name="methodCall">The method call.</param>
        /// <returns>The request data type</returns>
        private async Task<T> GetDataAsync<T>(Dictionary<string, string> queryData, string methodCall)
        {
            string url = GetCineworldUrl(methodCall, queryData);

            string response = await _httpClient.GetStringAsync(url);

            var responseItem = JsonConvert.DeserializeObject<T>(response);

            return responseItem;
        }

        #endregion
    }
}