using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Admin
{
    public class CityRequest : BaseRequest
    {
        public string Name { get; set; }

        public long StateId { get; set; }
    }
}
