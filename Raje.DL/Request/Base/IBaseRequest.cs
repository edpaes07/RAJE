namespace Raje.DL.Request.Admin.Base
{
    public interface IBaseRequest
    {
        long Id { get; set; }

        bool FlagActive { get; set; }
    }
}