using Raje.DL.Response.Base;
using System;
using System.Collections.Generic;

namespace Raje.DL.Response.Admin
{
    public class AssessmentResponse : BaseResponse
    {
        public int Grade { get; set; }

        public string Commentary { get; set; }
    }
}
