using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _8Puzzel
{
    class Node
    {
        public int depth;
        public int[] tiles;
        public Node parent;
        public double f;

        public Node()
        {
            this.tiles = new int[9];
        }
    }
}
