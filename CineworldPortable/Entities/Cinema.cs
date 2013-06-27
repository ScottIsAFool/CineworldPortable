using Newtonsoft.Json;
using PropertyChanged;

namespace CineworldPortable.Entities
{
    [ImplementPropertyChanged]
    public class Cinema
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cinema_url")]
        public string CinemaUrl { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("postcode")]
        public string PostCode { get; set; }

        [JsonProperty("telephone")]
        public string TelephoneNumber { get; set; }
    }
}