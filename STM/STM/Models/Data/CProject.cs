using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CProject
    {
        public CProject()
        {
            CRelease = new HashSet<CRelease>();
            CTask = new HashSet<CTask>();
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public string Description { get; set; }

        public CUser ManagerNavigation { get; set; }
        public ICollection<CRelease> CRelease { get; set; }
        public ICollection<CTask> CTask { get; set; }
    }
}
