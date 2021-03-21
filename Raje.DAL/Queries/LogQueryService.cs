using Raje.DAL.EF;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.DAL.Model;
using Raje.DL.Services.DAL.QueryServices;
using Raje.Infra.Const;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raje.DAL.Queries
{
    public class LogQueryService : ILogQueryService
    {
        private readonly EntityContext _db;

        public LogQueryService(EntityContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<string>> GetAllDistinctAPIs()
        {
            List<string> result = new List<string>();
            result = await _db.Log.Select(m => m.Api).AsNoTracking().Distinct().ToListAsync();
            return result;
        }
    }
}
