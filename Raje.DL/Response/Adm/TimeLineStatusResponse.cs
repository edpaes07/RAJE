using Raje.DL.Response.Base;
using System;

namespace Raje.DL.Response.Adm
{
    public class TimeLineStatusResponse : BaseResponse
    {
        public string Name { get; set; }

        public int Priority { get; set; }

        public string Color { get; set; }

        public DateTime? ConclusionDate { get; set; }

    }
}
