using Newtonsoft.Json;

namespace CineworldPortable.Entities
{
    public class Film
    {
        [JsonProperty("edi")]
        public int Edi { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("classification")]
        public string Classification { get; set; }

        [JsonProperty("advisory")]
        public string Advisory { get; set; }

        [JsonProperty("poster_url")]
        public string PosterUrl { get; set; }

        [JsonProperty("still_url")]
        public string StillUrl { get; set; }

        [JsonProperty("film_url")]
        public string FilmUrl { get; set; }
    }
}