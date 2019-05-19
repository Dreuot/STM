using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CTeam
    {
        public CTeam()
        {
            CTeamRole = new HashSet<CTeamRole>();
            CUserTeam = new HashSet<CUserTeam>();
        }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CTeamRole> CTeamRole { get; set; }
        public virtual ICollection<CUserTeam> CUserTeam { get; set; }
    }
}
