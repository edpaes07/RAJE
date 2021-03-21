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
using System.Linq;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Admin
{
    public class CityService : BaseCRUDBusinessService<City, CityResponse, CityRequest, CitySearchRequest, CityResponse>, ICityService
    {
        private readonly IRepository<City> _cityRepository;

        public CityService(IRepository<City> cityRepository
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor
            ) : base(cityRepository, mapper, httpContextAccessor)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<CityResponse>> CitiesByStateId(long idState)
        {
            IEnumerable<City> cities = await _cityRepository
                .Query()
                .Where(_ => _.StateId == idState && _.FlagActive)
                .OrderByAsc(_ => _.Name)
                .SearchAsync();
            var citiesResponse = _mapper.Map<IEnumerable<CityResponse>>(cities);
            return citiesResponse;
        }

        public async Task<long> GetIdByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Nome da cidade é obrigatório.");

            var cleanName = name.Trim();

            var cities = await _cityRepository
                .Query()
                .Where(s => s.FlagActive && cleanName.Equals(s.Name))
                .SearchAsync();

            var city = cities?.FirstOrDefault();

            if (city is null)
                throw new ArgumentNullException("Cidade não reconhecida.");

            return city.Id;
        }

        #region [Validation]

        public override async Task ValidateModelCreateRequest(CityRequest model, bool exists = false)
        {
            if (model is null)
                throw new ArgumentNullException("Cidade não reconhecida.");

            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ArgumentNullException("Nome da cidade é obrigatório.");

            if (await IsCityNameInUse(model.Name, model.Id))
                throw new InvalidOperationException("Este cidade já está em uso.");
        }

        private async Task<bool> IsCityNameInUse(string name, long? id)
        {
            int count = await _cityRepository
               .Query()
               .Where(city => city.FlagActive
                   && city.Id != id)
               .CountAsync();

            return count > 0;
        }

        #endregion

    }
}
