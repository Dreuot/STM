using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CTaskRel
    {
        public int Id { get; set; }
        public int? TaskMasterId { get; set; }
        public int? TaskSlaveId { get; set; }
        public string RelType { get; set; }

        public virtual CTask TaskMaster { get; set; }
        public virtual CTask TaskSlave { get; set; }
    }
}
