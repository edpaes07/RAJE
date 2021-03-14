using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Response.Admin;
using Raje.DL.Response.Base;
using Raje.DL.Services.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Base
{
    /*Interface padrão para os serviços de regra de negocios que implementam um CRUD*/
    /*Será usado para injeção de dependência automatica*/

    public interface ICRUDService<TEntity, TResponse, TRequest, TSearchRequest, TSearchResponse> : IDependencyInjectionService
        where TEntity : IEntity
        where TResponse : IBaseResponse
        where TRequest : IBaseRequest
        where TSearchRequest : IBaseSearchRequests
        where TSearchResponse : IBaseResponse
    {
        Task<IEnumerable<TResponse>> GetAll(bool? active);

        Task<TResponse> GetById(long id);

        Task<long> Create(TRequest model);

        Task Update(long id, TRequest model);

        void Delete(long id);

        Task ValidateModelCreateRequest(TRequest model, bool exists = false);

        Task<BaseSearchResponse<TSearchResponse>> Search(TSearchRequest search);
    }
}