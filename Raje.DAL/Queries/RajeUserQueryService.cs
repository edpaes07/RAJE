using Raje.DAL.EF;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.DAL.Model;
using Raje.DL.Services.DAL.QueryServices;
using Raje.Infra.Const;
using Raje.Infra.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raje.DAL.Queries
{
    public class UserQueryService : IUserQueryService
    {
        private readonly EntityContext _db;

        public UserQueryService(EntityContext db)
        {
            _db = db;
        }
    }
}
