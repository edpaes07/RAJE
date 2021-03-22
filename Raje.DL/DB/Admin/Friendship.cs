using Raje.DL.DB.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raje.DL.DB.Admin
{
    public class Friendship : EntityAuditBase
    {
        [ForeignKey("User")]
        public long UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Friend")]
        public long FriendId { get; set; }

        public virtual User Friend { get; set; }

        public bool IsValid { get; set; }
    }
}