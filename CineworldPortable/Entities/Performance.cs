using Newtonsoft.Json;
using PropertyChanged;

namespace CineworldPortable.Entities
{
    [ImplementPropertyChanged]
    public class Performance
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("booking_url")]
        public string BookingUrl { get; set; }

        [JsonProperty("subtitled")]
        public bool IsSubtitled { get; set; }

        [JsonProperty("ad")]
        public bool IsAudioDescribed { get; set; }
    }
}