using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CTeam
    {
        public CTeam()
        {
            CTeamRole = new HashSet<CTeamRole>();
            CUserTeam = new HashSet<CUserTeam>();
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CTeamRole> CTeamRole { get; set; }
        public ICollection<CUserTeam> CUserTeam { get; set; }
    }
}
