namespace Raje.DL.Services.DAL
{
    public interface IEntity
    {
        long Id { get; set; }

        bool FlagActive { get; set; }
    }
}