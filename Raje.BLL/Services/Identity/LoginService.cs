using Raje.DL.DB.Admin;
using Raje.DL.Request.Identity;
using Raje.DL.Response.Identity;
using Raje.DL.Services.BLL.Identity;
using Raje.DL.Services.DAL.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Admin
{
    public class UserNameService : IUserNameService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IResetPasswordService _resetPasswordService;

        public UserNameService(IRepository<User> repository
            , IMapper mapper
            , IPasswordHasher<User> passwordHasher
            , IJwtTokenService jwtTokenService
            , IResetPasswordService resetPasswordService
            )
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
            _resetPasswordService = resetPasswordService;
        }

        public async Task<LoginResponse> UserName(LoginRequest request)
        {
            var model = await _repository.Query()
                .Where(model => model.FlagActive && model.UserName == request.UserName)
                .IncludeEntity(_ => _.UserRole)
                .FirstOrDefaultAsync();

            if (model == null)
                throw new KeyNotFoundException("Usuário ou senha inválido.");
            var passwordVerification = _passwordHasher.VerifyHashedPassword(model, model.PasswordHash, request.Password);
            if (passwordVerification == PasswordVerificationResult.Failed)
                throw new KeyNotFoundException("Usuário ou senha inválido.");

            return GetLoginResponse(model, request.IsFirstAccessNeeded);
        }

        public async Task<LoginResponse> Refresh(RefreshTokenRequest request)
        {
            var model = await _repository.Query()
                .Where(model => model.FlagActive && model.UserName == request.UserName && model.RefreshToken == request.RefreshToken)
                .IncludeEntity(_ => _.UserRole)
                .FirstOrDefaultAsync();

            if (model == null)
                throw new KeyNotFoundException("RefreshToken inválido.");

            return GetLoginResponse(model, true);
        }

        private LoginResponse GetLoginResponse(User model, bool IsFirstAccessNeeded)
        {
            var response = new LoginResponse();
            response.FirstAccess = model.FirstAccess;
            if (model.FirstAccess && IsFirstAccessNeeded)
                response.Token = _resetPasswordService.EncrypterdToken(model.PasswordHash);
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

        public string GetLastGuidAuthentication(string loginName)
        {
            var model = _repository.Query()
               .Where(state => state.FlagActive
                   && state.UserName == loginName)
               .Search()
               .FirstOrDefault();
            return model.LastGuidAuthentication;
        }

        public string GetLastRefreshToken(string loginName)
        {
            var model = _repository.Query()
               .Where(state => state.FlagActive
                   && state.UserName == loginName)
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
