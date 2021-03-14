using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Base
{
    public interface IEmailSenderService : IDependencyInjectionService
    {
        Task SendAsync(string email, string subject, string message);
    }
}
