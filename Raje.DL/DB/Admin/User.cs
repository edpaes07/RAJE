using Raje.DL.DB.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raje.DL.DB.Admin
{
    public class User : EntityAuditBase
    {
        #region [ Personal Data ]

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string City { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        #endregion [ Personal Data ] 

        [MaxLength(36)]
        public string LastGuidAuthentication { get; set; }

        public bool FirstAccess { get; set; }

        [MaxLength(100)]
        public string RefreshToken { get; set; }

        [MaxLength(6)]
        public int VerificationCode { get; set; }

        [ForeignKey("Media")]
        public long? MediaId { get; set; }

        public virtual Media Media { get; set; }

        public virtual ICollection<Assessment> Assessment { get; set; }
    }
}
