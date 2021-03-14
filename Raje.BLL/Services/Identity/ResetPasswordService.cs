using Raje.DL.DB.Admin;
using Raje.DL.Request.Base;
using Raje.DL.Request.Identity;
using Raje.DL.Services.BLL.Base;
using Raje.DL.Services.BLL.Identity;
using Raje.DL.Services.DAL.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Identity
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IRepository<User> _repository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ResetPasswordOptions _resetPasswordOptions;
        private readonly ITimeLimitedDataProtector _timeLimitedDataProtector;
        private readonly IPasswordHasher<User> _passwordHasher;
        public readonly  IMapper _mapper;

        public ResetPasswordService(
            IRepository<User> repository,
            IOptions<ResetPasswordOptions> resetPasswordOptions,
            IEmailSenderService emailSenderService,
            IDataProtectionProvider dataProtectionProvider,
            IPasswordHasher<User> passwordHasher
        )
        {
            _repository = repository;
            _resetPasswordOptions = resetPasswordOptions.Value;
            _emailSenderService = emailSenderService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> SendEmail(BaseResetPasswordRequest request)
        {
            var user = _repository.Query()
                .Where(user => user.FlagActive && request.Cpf.Equals(user.Cpf))
                .Search()
                .FirstOrDefault();

            ValidateUserModel(user);

            GenerateVerificationCode(user);

            await SendUserEmail(user);

            return user.Email;
        }

        public void GenerateVerificationCode(User user)
        {
            user.VerificationCode = RandomNumberGenerator.GetInt32(100000, 999999);

            _repository.Update(user);
        }

        public async Task SendUserEmail(User user)
        {
            if (String.IsNullOrEmpty(user.Email))
                return;

            await _emailSenderService.SendAsync(
                user.Email,
                "AppSneakers - Redefina sua senha",
                $"Caro(a) {user.Name} <br>" +
                $"O código de verificação para redefinir sua senha é: {user.VerificationCode}"
            );
        }

        public void ResetPassword(ResetPasswordRequest request)
        {
            ValidateResetPasswordModel(request);

            var user = _repository.Query()
                .Where(user => user.FlagActive && request.VerificationCode.Equals(user.VerificationCode) && request.Cpf.Equals(user.Cpf))
                .Search()
                .FirstOrDefault();

            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            var passwordHash = _passwordHasher.HashPassword(user, request.Password);
            user.PasswordHash = passwordHash;
            user.FirstAccess = false;
            user.VerificationCode = 0;
            _repository.Update(user);
        }

        #region [Validation]

        public void ValidateVerificationCode(ValidateVerificationCodeRequest request)
        {
            ValidateUserResetPasswordModel(request);

            var user = _repository.Query()
                .Where(user => user.FlagActive && request.VerificationCode.Equals(user.VerificationCode) && request.Cpf.Equals(user.Cpf))
                .Search()
                .FirstOrDefault();

            if (user == null || user.VerificationCode == 0)
                throw new KeyNotFoundException("Código de verificação de senha inválido");
        }

        public void ValidateResetPasswordModel(ResetPasswordRequest user)
        {
            ValidateUserResetPasswordModel(user);

            if (user.VerificationCode == 0)
                throw new ArgumentNullException("Código de verificação de senha é obrigatório");

            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentNullException("Nova senha é obrigatória");
        }

        public void ValidateUserResetPasswordModel(BaseResetPasswordRequest user)
        {
            if (user == null)
                throw new ArgumentNullException("Registro de redefinição de senha inválido");

            if (string.IsNullOrWhiteSpace(user.Cpf))
                throw new ArgumentNullException("Login de usuário é obrigatório");
        }

        public void ValidateUserModel(User user)
        {
            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            if (string.IsNullOrEmpty(user.Email))
                throw new KeyNotFoundException("Email do usuário não encontrado");
        }

        #endregion

        public string EncryptedToken(string passwordHash)
        {
            var encryptedToken = _timeLimitedDataProtector.Protect(passwordHash, TimeSpan.FromMinutes(_resetPasswordOptions.ExpirationMinutes));
            var encodedEncryptedToken = WebUtility.UrlEncode(encryptedToken);
            return encodedEncryptedToken;
        }
    }
}
