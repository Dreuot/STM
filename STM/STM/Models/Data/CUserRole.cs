using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CUserRole
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }

        public virtual CRole Role { get; set; }
        public virtual CUser User { get; set; }
    }
}
