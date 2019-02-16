using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CAttach
    {
        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
