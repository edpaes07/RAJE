using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Request.Identity;
using Raje.DL.Services.BLL.Base;
using Raje.DL.Services.BLL.Identity;
using Raje.DL.Services.DAL.DataAccess;
using Raje.Infra.Util;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Admin
{
    public class ResetPasswordService : IResetPasswordService
    {

        private readonly IRepository<User> _repository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ResetPasswordOptions _resetPasswordOptions;
        private readonly ITimeLimitedDataProtector _timeLimitedDataProtector;
        private readonly IPasswordHasher<User> _passwordHasher;

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
            _timeLimitedDataProtector = dataProtectionProvider.CreateProtector(_resetPasswordOptions.TokenKey).ToTimeLimitedDataProtector();
            _passwordHasher = passwordHasher;
        }

        public void ResetPassword(ResetPasswordRequest request)
        {
            ValidateResetPasswordModel(request);

            var decryptedToken = TryDecryptToken(request);

            var model = _repository.Query()
                .Where(m => m.FlagActive && request.UserName.Equals(m.UserName))
                .Search()
                .FirstOrDefault();

            if (model == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            if (!decryptedToken.Equals(model.PasswordHash))
                throw new InvalidOperationException("Token de redefinição de senha inválido");

            var passwordHash = _passwordHasher.HashPassword(model, request.Password);
            model.PasswordHash = passwordHash;
            model.FirstAccess = false;
            _repository.Update(model);
        }

        public async Task SendEmail(ResetPasswordEmailRequest request)
        {
            ValidateSendEmailModel(request);

            var model = _repository.Query()
                .Where(m => m.FlagActive && request.UserName.Equals(m.UserName) && request.Email.Equals(m.Email))
                .Search()
                .FirstOrDefault();

            ValidateUserModel(model);

            await SendUserEmail(model);
        }

        public async Task ResetUserPassword(BaseResetPasswordRequest request)
        {
            ValidateUserResetPasswordModel(request);

            var model = _repository.Query()
                .Where(m => m.FlagActive && request.UserName.Equals(m.UserName))
                .Search()
                .FirstOrDefault();

            ValidateUserModel(model);

            await SendUserEmail(model);
        }

        public async Task SendUserEmail(User model)
        {
            if (String.IsNullOrEmpty(model.Email))
                return;

            var encryptedToken = EncrypterdToken(model.PasswordHash);

            await _emailSenderService.SendAsync(
                model.Email,
                "RAJE - Redefina sua senha",
                $"Caro(a) {model.FullName} <br>" +
                $"Para redefinir sua senha , <a href='{_resetPasswordOptions.Url}?token={encryptedToken}'>clique aqui</a>"
            );
        }

        private string TryDecryptToken(ResetPasswordRequest request)
        {
            string decryptedToken;

            try
            {
                decryptedToken = _timeLimitedDataProtector.Unprotect(request.Token);
            }
            catch (CryptographicException)
            {
                throw new InvalidOperationException("Token de redefinição de senha inválido");
            }

            if (string.IsNullOrEmpty(decryptedToken))
                throw new InvalidOperationException("Token de redefinição de senha inválido");

            return decryptedToken;
        }

        #region [Validation]

        public void ValidateSendEmailModel(ResetPasswordEmailRequest model)
        {
            ValidateUserResetPasswordModel(model);

            if (string.IsNullOrWhiteSpace(model.Email))
                throw new ArgumentNullException("E-mail do usuário é obrigatório");

            if (!EmailHelper.ValidarEmail(model.Email))
                throw new ArgumentNullException("E-mail do usuário é obrigatório");
        }

        public void ValidateResetPasswordModel(ResetPasswordRequest model)
        {
            ValidateUserResetPasswordModel(model);

            if (string.IsNullOrWhiteSpace(model.Token))
                throw new ArgumentNullException("Token de redefinição de senha é obrigatório");

            if (string.IsNullOrWhiteSpace(model.Password))
                throw new ArgumentNullException("Nova senha é obrigatório");

            if (string.IsNullOrWhiteSpace(model.ConfirmPassword))
                throw new ArgumentNullException("Confirmação de senha é obrigatório");

            if (!model.Password.Equals(model.ConfirmPassword))
                throw new ArgumentNullException("Confirmação de senha e senha não são equivalentes");

        }

        public void ValidateUserResetPasswordModel(BaseResetPasswordRequest model)
        {
            if (model is null)
                throw new ArgumentNullException("Registro de redefinição de senha inválido");

            if (string.IsNullOrWhiteSpace(model.UserName))
                throw new ArgumentNullException("UserName de usuário é obrigatório");
        }

        public void ValidateUserModel(User model)
        {
            if (model == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            if (string.IsNullOrEmpty(model.Email))
                throw new KeyNotFoundException("Email do usuário não encontrado");
        }

        #endregion


        public string EncrypterdToken(string passwordHash)
        {
            var encryptedToken = _timeLimitedDataProtector.Protect(passwordHash, TimeSpan.FromMinutes(_resetPasswordOptions.ExpirationMinutes));
            var encodedEncryptedToken = WebUtility.UrlEncode(encryptedToken);
            return encodedEncryptedToken;
        }
    }
}
