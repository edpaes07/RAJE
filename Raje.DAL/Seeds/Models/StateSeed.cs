using Newtonsoft.Json;

namespace Raje.DAL.Seeds.Models
{
    class StateSeed
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("sigla")]
        public string Abbreviation { get; set; }
    }
}
