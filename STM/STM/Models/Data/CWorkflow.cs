using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CWorkflow
    {
        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string RoleId { get; set; }
        public string StatusFromId { get; set; }
        public string StatusToId { get; set; }

        public CRole Role { get; set; }
        public CTaskStatus StatusFrom { get; set; }
        public CTaskStatus StatusTo { get; set; }
    }
}
