using Raje.BLL.Services.Base;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using Raje.DL.Services.DAL.DataAccess;
using Raje.Infra.Util;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Admin
{
    public class StateService : BaseCRUDBusinessService<State, StateResponse, StateRequest, StateSearchRequest, StateResponse>, IStateService
    {
        private readonly IRepository<State> _stateRepository;

        public StateService(IRepository<State> stateRepository
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor
            ) : base(stateRepository, mapper, httpContextAccessor)
        {
            _stateRepository = stateRepository;
        }

        public async Task<long> GetIdByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                throw new ArgumentNullException("Sigla do estado é obrigatória.");

            var cleanAbbreviation = abbreviation.RemoveSpecialCharacters(true, false);

            var states = await _stateRepository
                .Query()
                .Where(s => s.FlagActive && cleanAbbreviation.Equals(s.Abbreviation))
                .SearchAsync();

            var state = states?.FirstOrDefault();

            if (state is null)
                throw new ArgumentNullException("Estado não reconhecido.");

            return state.Id;
        }

        #region [Validation]

        public override async Task ValidateModelCreateRequest(StateRequest model, bool exists = false)
        {
            if (model is null)
                throw new ArgumentNullException("Estado não reconhecida.");

            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ArgumentNullException("Nome do estado é obrigatório.");

            if (await IsStateNameInUse(model.Name, model.Id))
                throw new InvalidOperationException("Este nome de estado já está em uso.");
        }

        private async Task<bool> IsStateNameInUse(string name, long? id)
        {
            int count = await _stateRepository
               .Query()
               .Where(state => state.FlagActive
                   && state.Id != id)
               .CountAsync();

            return count > 0;
        }

        #endregion


        public override async Task<IEnumerable<StateResponse>> GetAll(bool? active)
        {
            var query = _repository
                .Query()
                .Where(_ => _.FlagActive == active || active == null);

            var filterByUser = GetFilterByUser();
            if (filterByUser != null)
                query.Where(filterByUser);

            var models = await query.SearchAsync();

            var response = _mapper.Map<List<StateResponse>>(models);
            return response;
        }
    }
}
