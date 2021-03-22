using Newtonsoft.Json;
using System;

namespace Raje.DAL.Seeds.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    class MediaSeed
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("filePath")]
        public string FilePath { get; set; }

        [JsonProperty("folder")]
        public string Folder { get; set; }

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