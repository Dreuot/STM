using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CActivity
    {
        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string ActivityTypeId { get; set; }
        public string UserId { get; set; }
        public string TaskId { get; set; }
        public string Description { get; set; }

        public CActivityType ActivityType { get; set; }
        public CTask Task { get; set; }
        public CUser User { get; set; }
    }
}
