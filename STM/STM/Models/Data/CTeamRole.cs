using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CTeamRole
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public int? RoleId { get; set; }

        public virtual CRole Role { get; set; }
        public virtual CTeam Team { get; set; }
    }
}
