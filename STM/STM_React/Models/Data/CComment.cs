using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CComment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TaskId { get; set; }
        public DateTime? Created { get; set; }
        public string Comment { get; set; }
        public bool? Edited { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual CTask Task { get; set; }
        public virtual CUser User { get; set; }
    }
}
