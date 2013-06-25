using Newtonsoft.Json;

namespace CineworldPortable.Entities
{
    internal class CinemaResponse
    {
        [JsonProperty("cinemas")]
        public Cinema[] Cinemas { get; set; }
    }
}