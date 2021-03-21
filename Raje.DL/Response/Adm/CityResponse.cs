using Raje.DL.Response.Base;

namespace Raje.DL.Response.Adm
{
    public class CityResponse : BaseResponse
    {
        public string Name { get; set; }

        public long StateId { get; set; }

        public StateResponse State { get; set; }
    }
}
