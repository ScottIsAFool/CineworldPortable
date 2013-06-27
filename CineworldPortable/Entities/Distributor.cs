using Newtonsoft.Json;
using PropertyChanged;

namespace CineworldPortable.Entities
{
    [ImplementPropertyChanged]
    public class Distributor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}