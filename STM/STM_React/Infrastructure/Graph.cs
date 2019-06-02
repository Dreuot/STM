using STM_React.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM_React.Infrastructure
{
    public class Graph
    {
        public static readonly int Infinity = int.MinValue;
        private GraphNode start;

        public int Size { get; private set; }
        public int?[,] Matrix { get; private set; }
        public GraphNode[] Nodes { get; private set; }
        public GraphNode Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
                start.FinalResult = true;
                Reset();
            }
        }

        public GraphNode End { get; private set; }

        public Graph(int?[,] matrix)
        {
            Size = matrix.GetLength(0);
            Matrix = matrix;
            Nodes = new GraphNode[Size];
            for (int i = 0; i < Size; i++)
                Nodes[i] = new GraphNode(i, 0);

            Start = Nodes[0];
        }

        private void Reset()
        {
            for (int i = 0; i < Size; i++)
            {
                if (i != Start.Number)
                    Nodes[i].Distance = Infinity;
                else
                    Nodes[i].Distance = 0;
            }
        }

        public void Dynamic()
        {
            Reset();
            var s = Start;
            for (int i = 1; i < Size; i++)
            {
                var T = Nodes.Where(n => Matrix[n.Number, Nodes[i].Number] != null).ToArray();
                int old = Nodes[i].Distance;
                int count = T.Count();

                if (count > 0)
                {
                    int[] mins = new int[count];
                    for (int j = 0; j < count; j++)
                        mins[j] = T[j].Distance + Matrix[T[j].Number, Nodes[i].Number].Value;

                    int min = mins.Max();
                    Nodes[i].Distance = old < min ? min : old;
                }
            }
        }

        public int[] Distances()
        {
            return Nodes.Select(n => n.Distance).ToArray();
        }
    }
}
