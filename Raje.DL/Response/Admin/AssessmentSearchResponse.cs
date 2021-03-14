using Raje.DL.Response.Base;
using Raje.DL.Services.DAL;

namespace Raje.DL.Response.Admin
{
    public class AssessmentSearchResponse : BaseResponse, IEntity
    {
        public int Grade { get; set; }

        public string Commentary { get; set; }
    }
}
