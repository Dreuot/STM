using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CBoard
    {
        public CBoard()
        {
            CList = new HashSet<CList>();
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CList> CList { get; set; }
    }
}
