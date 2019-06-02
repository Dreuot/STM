using STM_React.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STM_React.Infrastructure
{
    public class GraphLevel
    {
        public List<CTask> Tasks { get; set; }
        public int Level { get; set; }
        public GraphLevel()
        {
            Tasks = new List<CTask>();
        }
    }
}
