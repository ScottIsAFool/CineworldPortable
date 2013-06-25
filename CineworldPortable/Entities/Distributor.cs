using Newtonsoft.Json;

namespace CineworldPortable.Entities
{
    public class Distributor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}