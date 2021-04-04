using Raje.DL.Request.Admin.Base;
using Raje.DL.Response.Base;
using Raje.DL.Services.BLL.Base;
using Raje.DL.Services.DAL.DataAccess;
using Raje.DL.Services.DAL.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Base
{
    public abstract class BaseCRUDBusinessService<TEntity, TResponse, TRequest, TSearchRequest, TSearchResponse> : BaseService, ICRUDBusinessService<TEntity, TResponse, TRequest, TSearchRequest, TSearchResponse>
        where TEntity : IEntity
        where TResponse : IBaseResponse
        where TRequest : IBaseRequest
        where TSearchRequest : IBaseSearchRequests
        where TSearchResponse : IBaseResponse
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IRepository<TEntity> _repository;
        public readonly IMapper _mapper;

        public BaseCRUDBusinessService(IRepository<TEntity> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public abstract Task ValidateModelCreateRequest(TRequest model, bool exists = false);

        public virtual async Task<IEnumerable<TResponse>> GetAll(bool? active)
        {
            var query = _repository
                .Query()
                .Where(_ => _.FlagActive == active || active == null);

            var filterByUser = GetFilterByUser();
            if (filterByUser != null)
                query.Where(filterByUser);

            var models = await query.SearchAsync();

            var response = _mapper.Map<List<TResponse>>(models);
            return response;
        }

        public virtual async Task<TResponse> GetById(long id)
        {
            TEntity model = await GetModel(id);
            var response = _mapper.Map<TResponse>(model);
            return response;
        }

        public virtual async Task<TEntity> GetModel(long id)
        {
            var query = _repository
                .Query()
                .Where(_ => _.Id == id);

            var filterByUser = GetFilterByUser();
            if (filterByUser != null)
                query.Where(filterByUser);

            TEntity model = await query
                .FirstOrDefaultAsync();

            if (model is null)
                throw new KeyNotFoundException(nameof(model));
            return model;
        }

        public virtual async Task<long> Create(TRequest request)
        {
            await ValidateModelCreateRequest(request);
            var model = _mapper.Map<TEntity>(request);
            _repository.Insert(model);
            return model.Id;
        }

        public virtual async Task Update(long id, TRequest request)
        {
            request.Id = id;
            await ValidateModelCreateRequest(request, true);
            TEntity model = await GetModel(id);
            var mapModel = _mapper.Map(request, model);
            _repository.Update(mapModel, true);
        }

        public virtual void Delete(long id)
        {
            TEntity model = _repository.FindById(id);
            if (model is null)
                throw new KeyNotFoundException(nameof(model));
            _repository.Delete(model);
        }

        public virtual async Task<BaseSearchResponse<TSearchResponse>> Search(TSearchRequest search)
        {
            IEnumerable<long> idsSelected = search.Ids != null ? search.Ids.Distinct().Where(id => id != 0) : null;
            IPageEntity<TEntity> searchResult = await _repository
                .Query()
                .Where(
                     model => (idsSelected == null || idsSelected.Contains(model.Id))
                    && (search.FlagActive == null || model.FlagActive == search.FlagActive))
                .PagedSearchAsync(search.PageIndex, search.PageSize);
            var searchResponse = _mapper.Map<BaseSearchResponse<TSearchResponse>>(searchResult);
            return searchResponse;
        }

        public virtual Expression<Func<TEntity, bool>> GetFilterByUser()
        {
            return null;
        }
    }
}
