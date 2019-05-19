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

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CTaskLabel> CTaskLabel { get; set; }
    }
}
