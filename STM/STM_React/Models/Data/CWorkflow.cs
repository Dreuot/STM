using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CWorkflow
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public int? RoleId { get; set; }
        public int? StatusFromId { get; set; }
        public int? StatusToId { get; set; }

        public virtual CRole Role { get; set; }
        public virtual CTaskStatus StatusFrom { get; set; }
        public virtual CTaskStatus StatusTo { get; set; }
    }
}
