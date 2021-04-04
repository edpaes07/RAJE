using Newtonsoft.Json;

namespace Raje.DAL.Seeds.Models
{
    class HassUserRoleSeed
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("systemCode")]
        public int SystemCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
