using Raje.BLL.Services.Base;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using Raje.DL.Services.DAL.DataAccess;
using Raje.DL.Services.DAL.Model;
using Raje.DL.Services.DAL.QueryServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Raje.BLL.Services.Admin
{
    public class LogService : BaseService, ILogService
    {
        private readonly IRepository<Log> _repository;
        private readonly IMapper _mapper;
        private readonly ILogQueryService _queryService;

        public LogService(IRepository<Log> repository
            , IMapper mapper
            , ILogQueryService queryService
            , IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _queryService = queryService;
        }

        public virtual LogResponse GetById(long id)
        {
            Log model = GetModel(id);
            var response = _mapper.Map<LogResponse>(model);
            return response;
        }

        public virtual long Create(LogRequest request)
        {
            var model = _mapper.Map<Log>(request);
            _repository.Insert(model);
            return model.Id;
        }

        private async Task<IEnumerable<LogReportResponse>> SearchLogsQuery(LogSearchRequest search)
        {
            IEnumerable<long> idsSelected = search.Ids != null ? search.Ids.Distinct().Where(id => id != 0) : null;

            var searchResult = await _repository
                .Query()
                .Where(
                    log => (idsSelected == null || idsSelected.Count() == 0 || idsSelected.Contains(log.Id))
                    && (search.Author == null || log.CreatedBy == search.Author)
                    && (search.Input == null || log.Input == search.Input)
                    && (search.Code == null || log.Code == search.Code)
                    && (search.Api == null || log.Api == search.Api)
                    && (search.UrlQuery == null || log.UrlQuery == search.UrlQuery)
                    && (search.Method == null || log.Method == search.Method)
                    && (search.Request == null || log.Request == search.Request)
                    && (search.Response == null || log.Response == search.Response)
                    && (search.DateBegin == null || log.CreatedAt >= search.DateBegin)
                    && (search.DateEnd == null || log.CreatedAt <= search.DateEnd)
                    && (search.FlagActive == null || log.FlagActive == search.FlagActive))
                .SearchAsync();

            var response = _mapper.Map<List<LogReportResponse>>(searchResult);

            return response;
        }

        private Log GetModel(long id)
        {
            Log model = _repository
                .Query()
                .Where(_ => _.Id == id)
                .Search()
                .FirstOrDefault();
            if (model is null)
                throw new KeyNotFoundException(nameof(model));
            return model;
        }

        public Task<IEnumerable<string>> GetAllDistinctAPIs()
        {
            return _queryService.GetAllDistinctAPIs();
        }

        public async Task<IEnumerable<LogReportResponse>> SearchLogs(LogSearchRequest search)
        {
            IEnumerable<LogReportResponse> searchResult = await SearchLogsQuery(search);

            return _mapper.Map<IEnumerable<LogReportResponse>>(searchResult);
        }
    }
}
