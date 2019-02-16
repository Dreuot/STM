using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CRelease
    {
        public CRelease()
        {
            CTask = new HashSet<CTask>();
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectId { get; set; }

        public CProject Project { get; set; }
        public ICollection<CTask> CTask { get; set; }
    }
}
