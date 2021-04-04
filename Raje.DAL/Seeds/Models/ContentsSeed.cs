using Newtonsoft.Json;
using System;

namespace Raje.DAL.Seeds.Models
{
    class ContentsSeed
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        [JsonProperty("mainCast")]
        public string MainCast { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("releaseYear")]
        public int ReleaseYear { get; set; }

        [JsonProperty("numberSeasons")]
        public int NumberSeasons { get; set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }

        [JsonProperty("isValid")]
        public bool IsValid { get; set; }

        [JsonProperty("mediaId")]
        public long? MediaId { get; set; }

        [JsonProperty("flagactive")]
        public bool FlagActive { get; set; }

        [JsonProperty("createdby")]
        public string CreatedBy { get; set; }

        [JsonProperty("createdat")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("modifiedby")]
        public string ModifiedBy { get; set; }

        [JsonProperty("modifiedat")]
        public DateTime ModifiedAt { get; set; }
    }
}
