using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CTaskLabel
    {
        public int Id { get; set; }
        public int? TaskId { get; set; }
        public int? LabelId { get; set; }
        public DateTime? Created { get; set; }

        public virtual CLabel Label { get; set; }
        public virtual CTask Task { get; set; }
    }
}
