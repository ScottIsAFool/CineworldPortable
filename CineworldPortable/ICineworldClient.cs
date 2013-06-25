using System.Collections.Generic;
using System.Threading.Tasks;
using CineworldPortable.Entities;

namespace CineworldPortable
{
    public interface ICineworldClient
    {
        /// <summary>
        ///     Gets or sets the API key.
        /// </summary>
        /// <value>
        ///     The API key.
        /// </value>
        string ApiKey
        {
            get;
            set;
        }

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
        Task<List<Cinema>> GetCinemasAsync(
            Territory territory = Territory.UnitedKingdom,
            bool fullDetails = false,
            List<int> filmEdis = null,
            List<string> dates = null,
            int? cinemaId = null,
            string categoryCode = null,
            string eventCode = null,
            int? distributorId = null);

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
        Task<List<Film>> GetFilmsAsync(
            Territory territory = Territory.UnitedKingdom,
            bool fullDetails = false,
            List<string> dates = null,
            List<int> cinemaIds = null,
            string categoryCode = null,
            string eventCode = null,
            int? filmId = null,
            int? distributorId = null);

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
        Task<List<string>> GetDatesAsync(
            Territory territory = Territory.UnitedKingdom,
            int? cinemaId = null,
            int? filmId = null,
            string categoryCode = null,
            string eventCode = null,
            int? distributorId = null);

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
        Task<List<Performance>> GetPerformancesAsync(
            int? cinemaId,
            int? filmId,
            string date,
            Territory territory = Territory.UnitedKingdom,
            string campaign = null);

        /// <summary>
        ///     Gets the categories.
        /// </summary>
        /// <returns>The list of film categories</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
        Task<List<Category>> GetCategoriesAsync();

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <returns>Returns the list of events</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
        Task<List<Event>> GetEventsAsync();

        /// <summary>
        /// Gets the distributors async.
        /// </summary>
        /// <returns>The list of film distributors</returns>
        /// <exception cref="System.NullReferenceException">Api Key cannot be null or empty</exception>
        Task<List<Distributor>> GetDistributorsAsync();
    }
}