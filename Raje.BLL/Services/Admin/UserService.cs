using Raje.BLL.Services.Base;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using Raje.DL.Services.BLL.Identity;
using Raje.DL.Services.DAL.DataAccess;
using Raje.DL.Services.DAL.Model;
using Raje.DL.Services.DAL.QueryServices;
using Raje.Infra.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Admin
{
    public class Userservice : BaseCRUDBusinessService<User, UserResponse, UserRequest, UserSearchRequest, UserSearchResponse>, IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserQueryService _queryService;
        private readonly IResetPasswordService _resetPassword;

        public Userservice(
              IRepository<User> afeetUserRepository
            , IMapper mapper
            , IPasswordHasher<User> passwordHasher
            , IUserQueryService queryService
            , IResetPasswordService resetPassword
            , IHttpContextAccessor httpContextAccessor
            ) : base(afeetUserRepository, mapper, httpContextAccessor)
        {
            _passwordHasher = passwordHasher;
            _queryService = queryService;
            _resetPassword = resetPassword;
        }

        public override async Task<User> GetModel(long id)
        {
            User model = await _repository
                .Query()
                .Where(_ => _.Id == id)
                .IncludeEntity("UserStores")
                .IncludeEntity("UserFranchiseeGroups")
                .IncludeEntity("UserBusinessUnits")
                .FirstOrDefaultAsync();
            if (model is null)
                throw new KeyNotFoundException(nameof(model));
            return model;
        }

        public override async Task<UserResponse> GetById(long id)
        {
            return await base.GetById(id);
        }

        public override async Task<long> Create(UserRequest request)
        {
            if (request.Id > 0)
            {
                await Update(request.Id, request);
                return request.Id;
            }
            await ValidateModelCreateRequest(request);
            var model = _mapper.Map<User>(request);
            _repository.Insert(model, true);

            SendUserEMail(request, model);

            return model.Id;
        }

        public override async Task Update(long id, UserRequest request)
        {
            await ValidateModelCreateRequest(request, true);
            User model = await GetModel(id);
            var mapModel = _mapper.Map(request, model);

            _repository.Update(mapModel, true);

            SendUserEMail(request, model);
        }

        public void SendUserEMail(UserRequest request, User model)
        {
            if (request.Password == null) return;
            _resetPassword.SendUserEmail(model);
        }

        public override async Task ValidateModelCreateRequest(UserRequest model, bool exists = false)
        {
            if (model is null)
                throw new ArgumentNullException("Usuário não reconhecida.");

            if (string.IsNullOrWhiteSpace(model.FullName))
                throw new ArgumentNullException("Nome do usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(model.UserName))
                throw new ArgumentNullException("UserName é obrigatório.");

            if (await IsUserNameInUse(model.UserName, model.Id))
                throw new ArgumentNullException("UserName já cadastrado.");

            if (model.Id == 0 && model.Password == null)
                throw new ArgumentNullException("Password é Obrigatório.");
        }

        private async Task<bool> IsUserNameInUse(string login, long? id)
        {
            int count = await _repository
               .Query()
               .Where(model => model.UserName == login
                   && model.Id != id)
               .CountAsync();

            return count > 0;
        }

        private async Task<bool> IsEmailInUse(string email, long? id)
        {
            int count = await _repository
               .Query()
               .Where(model => model.Email == email
                   && model.Id != id)
               .CountAsync();

            return count > 0;
        }
    }
}
