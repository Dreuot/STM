using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STM_React.Infrastructure
{
    public class GraphNode : IComparable<GraphNode>
    {
        public int Number { get; set; }
        public int Distance { get; set; }
        public bool FinalResult { get; set; }

        public GraphNode(int number, int distance)
        {
            Number = number;
            Distance = distance;
            FinalResult = false;
        }

        public int CompareTo(GraphNode other)
        {
            return Distance > other.Distance ? 1: Distance < other.Distance ? -1 : 0;
        }
    }
}
