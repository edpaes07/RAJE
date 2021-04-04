using Raje.DL.DB.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raje.DL.DB.Admin
{
    public class User : EntityAuditBase
    {
        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(320)]
        public string UserName { get; set; }

        [MaxLength(320)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [ForeignKey("City")]
        public long CityId { get; set; }

        public City City { get; set; }

        [ForeignKey("State")]
        public long StateId { get; set; }

        public State State { get; set; }

        [ForeignKey("UserRole")]
        public long UserRoleId { get; set; }

        public UserRole UserRole { get; set; }

        [MaxLength(36)]
        public string LastGuidAuthentication { get; set; }

        public bool FirstAccess { get; set; }

        [MaxLength(100)]
        public string RefreshToken { get; set; }

        [ForeignKey("Media")]
        public long? MediaId { get; set; }

        public virtual Media Media { get; set; }

        public virtual ICollection<Assessment> Assessment { get; set; }

        public virtual ICollection<Friendship> Friends { get; set; }
    }
}