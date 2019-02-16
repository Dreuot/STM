using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CTeamRole
    {
        public string Id { get; set; }
        public string TeamId { get; set; }
        public string RoleId { get; set; }

        public CRole Role { get; set; }
        public CTeam Team { get; set; }
    }
}
