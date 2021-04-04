using Raje.DL._Base;
using System.Threading.Tasks;
using Xunit;

namespace Raje.Base.Tests._Utils
{
    public static class AssertExtension
    {
        public static void WithMessage(this DomainException exception, string mensagem)
        {
            if (exception.MessageErrors.Contains(mensagem))
                Assert.True(true);
            else
                Assert.False(true, $"Esperava a mensagem '{mensagem}'");
        }

        public static async Task WithMessage(this Task<DomainException> exception, string mensagem)
        {
            if ((await exception).MessageErrors.Contains(mensagem))
                Assert.True(true);
            else
                Assert.False(true, $"Esperava a mensagem '{mensagem}'");
        }
    }
}
