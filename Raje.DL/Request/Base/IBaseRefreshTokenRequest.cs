using System;
using System.Collections.Generic;
using System.Text;

namespace Raje.DL.Request.Base
{
    public interface IBaseRefreshTokenRequest
    {
        string Login { get; set; }

        public string RefreshToken { get; set; }
    }
}