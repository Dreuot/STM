using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CActivity
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public int? ActivityTypeId { get; set; }
        public int? UserId { get; set; }
        public int? TaskId { get; set; }
        public string Description { get; set; }

        public virtual CActivityType ActivityType { get; set; }
        public virtual CTask Task { get; set; }
        public virtual CUser User { get; set; }
    }
}
