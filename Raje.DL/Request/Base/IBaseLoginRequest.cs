using System;
using System.Collections.Generic;
using System.Text;

namespace Raje.DL.Request.Base
{
    public interface IBaseLoginRequest
    {
        string Login { get; set; }

        string Password { get; set; }
    }
}