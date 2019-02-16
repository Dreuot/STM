using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CTaskPriority
    {
        public CTaskPriority()
        {
            CTask = new HashSet<CTask>();
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public ICollection<CTask> CTask { get; set; }
    }
}
