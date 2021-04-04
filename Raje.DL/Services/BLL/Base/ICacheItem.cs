namespace Raje.DL.Services.BLL.Base
{
    public interface ICacheItem<T>
    {
        T Item { get; set; }
        bool Valid { get; set; }
    }
}
