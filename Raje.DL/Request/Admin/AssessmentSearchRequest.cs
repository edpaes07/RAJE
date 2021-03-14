using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Admin
{
    public class AssessmentSearchRequest : BaseSearchRequest
    {
        public int Grade { get; set; }

        public string Commentary { get; set; }
    }
}
