using Newtonsoft.Json;

namespace CineworldPortable.Entities
{
    public class Category
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}