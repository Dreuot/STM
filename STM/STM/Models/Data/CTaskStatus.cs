using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CTaskStatus
    {
        public CTaskStatus()
        {
            CTask = new HashSet<CTask>();
            CWorkflowStatusFrom = new HashSet<CWorkflow>();
            CWorkflowStatusTo = new HashSet<CWorkflow>();
        }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Stage { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<CTask> CTask { get; set; }
        public virtual ICollection<CWorkflow> CWorkflowStatusFrom { get; set; }
        public virtual ICollection<CWorkflow> CWorkflowStatusTo { get; set; }
    }
}
