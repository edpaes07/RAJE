using Newtonsoft.Json;

namespace Raje.DAL.Seeds.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    class CitySeed
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("microrregiao.mesorregiao.UF.id")]
        public long StateId { get; set; }
    }
}
