using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CRelease
    {
        public CRelease()
        {
            CTask = new HashSet<CTask>();
        }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ProjectId { get; set; }

        public virtual CProject Project { get; set; }
        public virtual ICollection<CTask> CTask { get; set; }
    }
}
