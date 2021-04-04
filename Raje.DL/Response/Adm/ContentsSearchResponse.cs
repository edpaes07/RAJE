using Raje.DL.DB.Admin;
using Raje.DL.Response.Base;
using Raje.DL.Services.DAL.Model;
using System.Collections.Generic;

namespace Raje.DL.Response.Adm
{
    public class ContentsSearchResponse : BaseResponse, IEntity
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

        public string Synopsis { get; set; }

        public bool IsValid { get; set; }

        public string Media { get; set; }

        public virtual ICollection<Assessment> Assessment { get; set; }
    }
}