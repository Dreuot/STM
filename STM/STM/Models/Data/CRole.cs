using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CRole
    {
        public CRole()
        {
            CTeamRole = new HashSet<CTeamRole>();
            CUserRole = new HashSet<CUserRole>();
            CWorkflow = new HashSet<CWorkflow>();
        }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CTeamRole> CTeamRole { get; set; }
        public virtual ICollection<CUserRole> CUserRole { get; set; }
        public virtual ICollection<CWorkflow> CWorkflow { get; set; }
    }
}
