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

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public ICollection<CTask> CTask { get; set; }
        public ICollection<CWorkflow> CWorkflowStatusFrom { get; set; }
        public ICollection<CWorkflow> CWorkflowStatusTo { get; set; }
    }
}
