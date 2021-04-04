using Raje.BLL.Services.Base;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using Raje.DL.Services.DAL.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Raje.BLL.Services.Admin
{
    public class ContentsService : BaseCRUDBusinessService<Contents, ContentsResponse, ContentsRequest, ContentsSearchRequest, ContentsSearchResponse>, IContentsService
    {
        private readonly IRepository<Contents> _contentsRepository;
        private readonly IConfiguration _configuration;

        public ContentsService(
            IRepository<Contents> contentsRepository,
            IMapper mapper,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
        ) : base(contentsRepository, mapper, httpContextAccessor)
        {
            _contentsRepository = contentsRepository;
            _configuration = configuration;
        }


        public override async Task<Contents> GetModel(long id)
        {
            Contents contents = await _contentsRepository
               .Query()
               .Where(contents => contents.Id == id)
                .IncludeEntity("Media")
                .IncludeEntity("Assessment")
               .FirstOrDefaultAsync();

            if (contents is null)
                throw new KeyNotFoundException(nameof(contents));

            return contents;
        }

        public async Task<IEnumerable<Contents>> GetAll(bool? active)
        {
            var query = _contentsRepository
                .Query()
                .Where(contents => active == null || contents.FlagActive == active)
                .IncludeEntity("Media")
                .IncludeEntity("Assessment");

            IEnumerable<Contents> contents = await query.SearchAsync();

            return contents;
        }

        public override async Task<long> Create(ContentsRequest request)
        {
            if (request.Id > 0)
            {
                await Update(request.Id, request);
                return request.Id;
            }
            await ValidateModelCreateRequest(request);
            var model = _mapper.Map<Contents>(request);
            _repository.Insert(model, true);

            return model.Id;
        }

        public override async Task Update(long id, ContentsRequest request)
        {
            await ValidateModelCreateRequest(request, true);
            Contents model = await GetModel(id);
            var mapModel = _mapper.Map(request, model);

            _repository.Update(mapModel, true);
        }


        public override async Task ValidateModelCreateRequest(ContentsRequest model, bool exists = false)
        {
            if (model is null)
                throw new ArgumentNullException("Conteúdo não reconhecida.");

            if (string.IsNullOrWhiteSpace(model.Title))
                throw new ArgumentNullException("Título do Conteúdo é obrigatório.");

            if (string.IsNullOrWhiteSpace(model.Title))
                throw new ArgumentNullException("ContentsName é obrigatório.");

            if (await IsTitleNameInUse(model.Title, model.Id))
                throw new ArgumentNullException("ContentsName já cadastrado.");
        }

        private async Task<bool> IsTitleNameInUse(string title, long? id)
        {
            int count = await _repository
               .Query()
               .Where(model => model.Title == title
                   && model.Id != id)
               .CountAsync();

            return count > 0;
        }
    }
}