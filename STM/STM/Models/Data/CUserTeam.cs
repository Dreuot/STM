using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CUserTeam
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TeamId { get; set; }

        public virtual CTeam Team { get; set; }
        public virtual CUser User { get; set; }
    }
}
