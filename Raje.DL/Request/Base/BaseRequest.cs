namespace Raje.DL.Request.Admin.Base
{
    public class BaseRequest : IBaseRequest
    {
        public long Id { get; set; }

        public bool FlagActive { get; set; } = true;
    }
}
