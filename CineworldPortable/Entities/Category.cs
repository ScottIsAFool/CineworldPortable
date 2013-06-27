using Newtonsoft.Json;
using PropertyChanged;

namespace CineworldPortable.Entities
{
    [ImplementPropertyChanged]
    public class Category
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}