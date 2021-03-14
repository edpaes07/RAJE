using Raje.DL.Request.Admin.Base;
using Microsoft.AspNetCore.Http;

namespace Raje.DL.Request.Admin
{
    public class AssessmentRequest : BaseRequest
    {
        public int Grade { get; set; }

        public string Commentary { get; set; }
    }
}
