using Newtonsoft.Json;
using System;

namespace Raje.DAL.Seeds.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    class UserSeed
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("passwordhash")]
        public string PasswordHash { get; set; }

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("cityId")]
        public long CityId { get; set; }

        [JsonProperty("stateId")]
        public long StateId { get; set; }

        [JsonProperty("userRoleId")]
        public long UserRoleId { get; set; }

        [JsonProperty("lastguidauthentication")]
        public string LastGuidAuthentication { get; set; }

        [JsonProperty("firstaccess")]
        public bool FirstAccess { get; set; }

        [JsonProperty("refreshtoken")]
        public string RefreshToken { get; set; }

        [JsonProperty("mediaId")]
        public long MediaId { get; set; }

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