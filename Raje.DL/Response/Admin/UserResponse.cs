using Raje.DL.Response.Base;
using System;
using System.Collections.Generic;

namespace Raje.DL.Response.Admin
{
    public class UserResponse : BaseResponse
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public DateTime BirthDate { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public IEnumerable<AssessmentResponse> Assessments { get; set; }
    }
}
