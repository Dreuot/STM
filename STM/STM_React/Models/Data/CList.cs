using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CList
    {
        public CList()
        {
            CTask = new HashSet<CTask>();
        }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? BoardId { get; set; }

        public virtual CBoard Board { get; set; }
        public virtual ICollection<CTask> CTask { get; set; }
    }
}
