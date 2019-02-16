using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CList
    {
        public CList()
        {
            CTask = new HashSet<CTask>();
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BoardId { get; set; }

        public CBoard Board { get; set; }
        public ICollection<CTask> CTask { get; set; }
    }
}
