using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CUserTeam
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TeamId { get; set; }

        public CTeam Team { get; set; }
        public CUser User { get; set; }
    }
}
