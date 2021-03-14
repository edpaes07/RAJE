using Raje.DL.DB.Admin;
using Raje.DL.Request.Base;
using Raje.DL.Response.Identity;
using Raje.DL.Services.BLL.Identity;
using Raje.DL.Services.DAL.DataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Identity
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IResetPasswordService _resetPasswordService;

        public LoginService(IRepository<User> repository
            , IPasswordHasher<User> passwordHasher
            , IJwtTokenService jwtTokenService
            , IResetPasswordService resetPasswordService
            )
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
            _resetPasswordService = resetPasswordService;
        }

        public async Task<LoginResponse> Login(BaseLoginRequest request)
        {
            var model = await _repository.Query()
                .Where(model => model.FlagActive && (model.Cpf == request.Login || model.Email == request.Login))
                .FirstOrDefaultAsync();

            if (model == null)
                throw new KeyNotFoundException("Usuário ou senha inválido.");
            var passwordVerification = _passwordHasher.VerifyHashedPassword(model, model.PasswordHash, request.Password);
           
            if (passwordVerification == PasswordVerificationResult.Failed)
                throw new KeyNotFoundException("Usuário ou senha inválido.");

            return GetLoginResponse(model);
        }

        public async Task<LoginResponse> Refresh(BaseRefreshTokenRequest request)
        {
            var model = await _repository.Query()
                .Where(model => model.FlagActive && (model.Cpf == request.Login || model.Email == request.Login) && model.RefreshToken == request.RefreshToken)
                .FirstOrDefaultAsync();

            if (model == null)
                throw new KeyNotFoundException("RefreshToken inválido.");

            return GetLoginResponse(model);
        }

        private LoginResponse GetLoginResponse(User model)
        {
            var response = new LoginResponse();
            response.FirstAccess = model.FirstAccess;
            if (model.FirstAccess)
                response.Token = _resetPasswordService.EncryptedToken(model.PasswordHash);
            else
            {
                UpdateUserAccessInfo(model);
                response.Token = _jwtTokenService.GenerateJwtToken(model);
                response.RefreshToken = model.RefreshToken;
            }
            return response;
        }

        private void UpdateUserAccessInfo(User model)
        {
            model.LastGuidAuthentication = Guid.NewGuid().ToString();
            model.RefreshToken = GenerateRefreshToken();
            _repository.Update(model);
        }

        public string GetLastGuidAuthentication(long id)
        {
            var model = _repository.Query()
               .Where(state => state.FlagActive
                   && (state.Id == id ))
               .Search()
               .FirstOrDefault();
            return model.LastGuidAuthentication;
        }

        public string GetLastRefreshToken(long id)
        {
            var model = _repository.Query()
               .Where(state => state.FlagActive
                   && (state.Id == id))
               .Search()
               .FirstOrDefault();
            return model.RefreshToken;
        }

        private string GenerateRefreshToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}