using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CComment
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TaskId { get; set; }
        public DateTime? Created { get; set; }
        public string Comment { get; set; }
        public bool? Edited { get; set; }
        public DateTime? LastUpdate { get; set; }

        public CTask Task { get; set; }
        public CUser User { get; set; }
    }
}
