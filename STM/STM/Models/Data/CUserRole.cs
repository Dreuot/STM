using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CUserRole
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public CRole Role { get; set; }
        public CUser User { get; set; }
    }
}
