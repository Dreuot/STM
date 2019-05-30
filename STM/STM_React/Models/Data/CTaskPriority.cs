﻿using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CTaskPriority
    {
        public CTaskPriority()
        {
            CTask = new HashSet<CTask>();
        }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int? PriorNum { get; set; }

        public virtual ICollection<CTask> CTask { get; set; }
    }
}
