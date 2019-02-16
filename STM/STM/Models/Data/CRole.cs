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

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CTeamRole> CTeamRole { get; set; }
        public ICollection<CUserRole> CUserRole { get; set; }
        public ICollection<CWorkflow> CWorkflow { get; set; }
    }
}
