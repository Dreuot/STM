using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CLabel
    {
        public CLabel()
        {
            CTaskLabel = new HashSet<CTaskLabel>();
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }

        public ICollection<CTaskLabel> CTaskLabel { get; set; }
    }
}
