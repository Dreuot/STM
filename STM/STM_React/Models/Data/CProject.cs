using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CProject
    {
        public CProject()
        {
            CRelease = new HashSet<CRelease>();
            CTask = new HashSet<CTask>();
        }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public int? Manager { get; set; }
        public string Description { get; set; }

        public virtual CUser ManagerNavigation { get; set; }
        public virtual ICollection<CRelease> CRelease { get; set; }
        public virtual ICollection<CTask> CTask { get; set; }
    }
}
