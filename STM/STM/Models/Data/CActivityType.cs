using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CActivityType
    {
        public CActivityType()
        {
            CActivity = new HashSet<CActivity>();
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CActivity> CActivity { get; set; }
    }
}
