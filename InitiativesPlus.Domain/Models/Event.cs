using Newtonsoft.Json;

namespace InitiativesPlus.Domain.Models
{
    public class Event
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Type { get; set; }
        public string Month { get; set; }
        public string Initiative { get; set; }
        public int Value { get; set; }
        public string InitiativeYear { get; set; }
    }
}
