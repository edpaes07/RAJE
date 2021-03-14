using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Admin
{
    public class ContentsRequest : BaseRequest
    {
        public string Type { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Director { get; set; }

        public string MainCast { get; set; }

        public string Country { get; set; }

        public int ReleaseYear { get; set; }

        public int NumberSeasons { get; set; }

        public bool IsValid { get; set; }
    }
}
