namespace Raje.DL.Services.DAL.Model
{
    public interface IEntity
    {
        long Id { get; set; }

        bool FlagActive { get; set; }
    }
}