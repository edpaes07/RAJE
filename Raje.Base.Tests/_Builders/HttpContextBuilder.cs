using Bogus;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using Raje.Infra.Enums;
using Raje.Infra.Util;
using Raje.Infra.Const;
using System.Linq;

namespace Raje.Base.Tests._Builders
{
    public class MockHttpContextAccessorBuilder
    {
        protected string Name;
        protected List<Claim> Claims;

        public static MockHttpContextAccessorBuilder New()
        {
           var  _faker = new Faker();
            return new MockHttpContextAccessorBuilder
            {
                Name = _faker.Name.FullName(),
                Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, _faker.Random.Long().ToString()),                    
                    new Claim(ClaimTypes.Name, _faker.Random.Word()),
                    new Claim(UserClaims.UserLastGuid,_faker.Random.Guid().ToString())
                }
            };

        }

        public MockHttpContextAccessorBuilder WithAdminUser()
        {
            Claims.Add(new Claim(ClaimTypes.Role, UserRoleTypes.AdminMaster.GetDescription()));

            return this;
        }

        public MockHttpContextAccessorBuilder WithUserName(string userName)
        {
            int index = Claims.FindIndex(_ => _.Type == ClaimTypes.Name);
            Claims.RemoveAt(index);
            Claims.Add(new Claim(ClaimTypes.Name, userName));
            return this;
        }

        public MockHttpContextAccessorBuilder WithUserId(long userId)
        {
            int index = Claims.FindIndex(_ => _.Type == ClaimTypes.NameIdentifier);
            Claims.RemoveAt(index);
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            return this;
        }

        public Mock<IHttpContextAccessor> Build()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var identity = new ClaimsIdentity(Claims, "TestAuthType");
            var defaultHttpContext = new DefaultHttpContext();
            var claimsPrincipal = new ClaimsPrincipal(identity);
            defaultHttpContext.User = claimsPrincipal;
            httpContextAccessor.SetupGet(ca => ca.HttpContext).Returns(defaultHttpContext);

            return httpContextAccessor;
        }
    }
}
