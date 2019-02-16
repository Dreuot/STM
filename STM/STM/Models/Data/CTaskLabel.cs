using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CTaskLabel
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string LabelId { get; set; }
        public DateTime? Created { get; set; }

        public CLabel Label { get; set; }
        public CTask Task { get; set; }
    }
}
