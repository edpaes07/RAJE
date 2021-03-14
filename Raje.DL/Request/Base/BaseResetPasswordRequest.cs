using System;
using System.Collections.Generic;
using System.Text;

namespace Raje.DL.Request.Base
{
    public class BaseResetPasswordRequest : IBaseResetPasswordRequest
    {
        public string Cpf { get; set; }
    }
}
