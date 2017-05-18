using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _8Puzzel
{
    class AStarSolver
    {
        Node start = null;
        Node goal = null;
        List<Node> output = null;
        List<Node> openlist = null;
        List<Node> closelist = null;
        List<KeyValuePair<int,Node>> tree = null;

        public AStarSolver(Node pstart, Node pgoal)
        {
            start = pstart;
            goal = pgoal;
            output = new List<Node>();
            openlist = new List<Node>();
            closelist = new List<Node>();
            tree = new List<KeyValuePair<int, Node>>();
            
        }

        public List<Node> solvePuzzel() {
            openlist.Add(this.start);
            
            int d = 0;
            tree.Add(new KeyValuePair<int,Node>(d,this.start));
            
            Node solved = null;
            while (openlist.Count > 0)
            {
                Node current = getMinimumF(openlist);
                if (areArraysEqual(current.tiles,goal.tiles))
                {
                    //Console.WriteLine(current.tiles[0] + " " + current.tiles[1] + " " + current.tiles[2] + " " + current.tiles[3] + " " + current.tiles[4] + " " +
                    //current.tiles[5] + " " + current.tiles[6] + " " + current.tiles[7] + " " + current.tiles[8]);
                    //Console.WriteLine("done");
                    solved = current;
                    break;
                }
                d++;
                List<Node> successors = getAllSuccesors(current);

                foreach (Node item in successors)
                {
                    //tree.Add(new KeyValuePair<int, Node>(d, item));
                    int depth_temp = current.depth + 1;

                    double f_temp = depth_temp + Huristic.ManhattanDistance(item.tiles, this.goal.tiles);
                    //double f_temp = depth_temp + Huristic.DistanceN(item.tiles, this.goal.tiles);
                    if (openlist.Contains(item) && f_temp < item.f)
                    {
                        Console.WriteLine("con1");
                        continue;

                    }

                    if (closelist.Contains(item) && f_temp < item.f)
                    {
                        Console.WriteLine("con2");
                        continue;
                    }

                    openlist.Remove(current);
                    closelist.Remove(current);
                    item.parent = current;
                    item.f = f_temp;
                    if (!nodeExists(openlist,item))
                    {
                        openlist.Add(item);    
                    }
                    

                }
                closelist.Add(current);
                //Console.WriteLine(current.tiles[0]+" "+current.tiles[1]+" "+current.tiles[2]+" "+current.tiles[3]+" "+current.tiles[4]+" "+
                //    current.tiles[5]+" "+current.tiles[6]+" "+current.tiles[7]+" "+current.tiles[8]);
            }

            while (solved != null)
            {
                output.Add(solved);
                solved = solved.parent;
            }

            //Thread.Sleep(10000);
            return output;
        }

        public Node getMinimumF(List<Node> opens)
        {
            Node output = opens.First<Node>();
            foreach (Node item in opens)
            {
                if (item.f < output.f)
                {
                    output = item;
                }
            }
            return output;
            
        }

        public bool nodeExists(List<Node> opens, Node node)
        {
            foreach (Node item in opens)
            {
                if (areArraysEqual(item.tiles,node.tiles))
                {
                    return true;
                }
            }
            return false;
            
        }

        public bool areArraysEqual(int[] a, int[] b)
        {
            
            for (int i = 0; i < a.Length; i++)
			{
                if (a[i] != b[i])
                {
                    return false;
                }
			}
            return true;
        
        }

        private List<Node> getAllSuccesors(Node pCurrent)
        {
            // Change them all like 3
            List<Node> output = new List<Node>();
            int index = -1;
            for (int i = 0; i < pCurrent.tiles.Length; i++)
            {
                if (pCurrent.tiles[i] == 0)
                {
                    index = i;
                    break;
                }
            }

            if (index == 0)
            {
                Node temp1 = new Node();
                //pCurrent.tiles.CopyTo(temp1.tiles,0);
                Array.Copy(pCurrent.tiles, temp1.tiles,9);
                temp1.tiles[0] = temp1.tiles[1];
                temp1.tiles[1] = 0;
                temp1.parent = pCurrent;
                output.Add(temp1);

                Node temp2 = new Node();
                Array.Copy(pCurrent.tiles, temp2.tiles, 9);
                temp2.tiles[0] = temp2.tiles[3];
                temp2.tiles[3] = 0;
                temp2.parent = pCurrent;
                output.Add(temp2);
            }
            else if (index == 1)
            {
                Node temp1 = new Node();
                Array.Copy(pCurrent.tiles, temp1.tiles, 9);
                temp1.tiles[1] = temp1.tiles[2];
                temp1.tiles[2] = 0;
                temp1.parent = pCurrent;
                output.Add(temp1);

                Node temp2 = new Node();
                Array.Copy(pCurrent.tiles, temp2.tiles, 9);
                temp2.tiles[1] = temp2.tiles[0];
                temp2.tiles[0] = 0;
                temp2.parent = pCurrent;
                output.Add(temp2);

                Node temp3 = new Node();
                Array.Copy(pCurrent.tiles, temp3.tiles, 9);
                temp3.tiles[1] = temp3.tiles[4];
                temp3.tiles[4] = 0;
                temp3.parent = pCurrent;
                output.Add(temp3);
            }
            else if (index == 2)
            {
                Node temp1 = new Node();
                Array.Copy(pCurrent.tiles, temp1.tiles, 9);
                temp1.tiles[2] = temp1.tiles[1];
                temp1.tiles[1] = 0;
                temp1.parent = pCurrent;
                output.Add(temp1);

                Node temp2 = new Node();
                Array.Copy(pCurrent.tiles, temp2.tiles, 9);
                temp2.tiles[2] = temp2.tiles[5];
                temp2.tiles[5] = 0;
                temp2.parent = pCurrent;
                output.Add(temp2);

            }
            else if (index == 3)
            {
                Node temp1 = new Node();
                Array.Copy(pCurrent.tiles, temp1.tiles, 9);
                temp1.tiles[3] = temp1.tiles[0];
                temp1.tiles[0] = 0;
                temp1.parent = pCurrent;
                output.Add(temp1);

                Node temp2 = new Node();
                Array.Copy(pCurrent.tiles, temp2.tiles, 9);
                temp2.tiles[3] = temp2.tiles[4];
                temp2.tiles[4] = 0;
                temp2.parent = pCurrent;
                output.Add(temp2);

                Node temp3 = new Node();
                Array.Copy(pCurrent.tiles, temp3.tiles, 9);
                temp3.tiles[3] = temp3.tiles[6];
                temp3.tiles[6] = 0;
                temp3.parent = pCurrent;
                output.Add(temp3);
            }
            else if (index == 4)
            {
                Node temp1 = new Node();
                Array.Copy(pCurrent.tiles, temp1.tiles, 9);
                temp1.tiles[4] = temp1.tiles[1];
                temp1.tiles[1] = 0;
                temp1.parent = pCurrent;
                output.Add(temp1);

                Node temp2 = new Node();
                Array.Copy(pCurrent.tiles, temp2.tiles, 9);
                temp2.tiles[4] = temp2.tiles[3];
                temp2.tiles[3] = 0;
                temp2.parent = pCurrent;
                output.Add(temp2);

                Node temp3 = new Node();
                Array.Copy(pCurrent.tiles, temp3.tiles, 9);
                temp3.tiles[4] = temp3.tiles[5];
                temp3.tiles[5] = 0;
                temp3.parent = pCurrent;
                output.Add(temp3);

                Node temp4 = new Node();
                Array.Copy(pCurrent.tiles, temp4.tiles, 9);
                temp4.tiles[4] = temp4.tiles[7];
                temp4.tiles[7] = 0;
                temp4.parent = pCurrent;
                output.Add(temp4);
            }
            else if (index == 5)
            {
                Node temp1 = new Node();
                Array.Copy(pCurrent.tiles, temp1.tiles, 9);
                temp1.tiles[5] = temp1.tiles[2];
                temp1.tiles[2] = 0;
                temp1.parent = pCurrent;
                output.Add(temp1);

                Node temp2 = new Node();
                Array.Copy(pCurrent.tiles, temp2.tiles, 9);
                temp2.tiles[5] = temp2.tiles[4];
                temp2.tiles[4] = 0;
                temp2.parent = pCurrent;
                output.Add(temp2);

                Node temp3 = new Node();
                Array.Copy(pCurrent.tiles, temp3.tiles, 9);
                temp3.tiles[5] = temp3.tiles[8];
                temp3.tiles[8] = 0;
                temp3.parent = pCurrent;
                output.Add(temp3);
            }
            else if (index == 6)
            {
                Node temp1 = new Node();
                Array.Copy(pCurrent.tiles, temp1.tiles, 9);
                temp1.tiles[6] = temp1.tiles[3];
                temp1.tiles[3] = 0;
                temp1.parent = pCurrent;
                output.Add(temp1);

                Node temp2 = new Node();
                Array.Copy(pCurrent.tiles, temp2.tiles, 9);
                temp2.tiles[6] = temp2.tiles[7];
                temp2.tiles[7] = 0;
                temp2.parent = pCurrent;
                output.Add(temp2);

            }
            else if (index == 7)
            {
                Node temp1 = new Node();
                Array.Copy(pCurrent.tiles, temp1.tiles, 9);
                temp1.tiles[7] = temp1.tiles[6];
                temp1.tiles[6] = 0;
                temp1.parent = pCurrent;
                output.Add(temp1);

                Node temp2 = new Node();
                Array.Copy(pCurrent.tiles, temp2.tiles, 9);
                temp2.tiles[7] = temp2.tiles[4];
                temp2.tiles[4] = 0;
                temp2.parent = pCurrent;
                output.Add(temp2);

                Node temp3 = new Node();
                Array.Copy(pCurrent.tiles, temp3.tiles, 9);
                temp3.tiles[7] = temp3.tiles[8];
                temp3.tiles[8] = 0;
                temp3.parent = pCurrent;
                output.Add(temp3);
            }
            else if (index == 8)
            {
                Node temp1 = new Node();
                Array.Copy(pCurrent.tiles, temp1.tiles, 9);
                temp1.tiles[8] = temp1.tiles[5];
                temp1.tiles[5] = 0;
                temp1.parent = pCurrent;
                output.Add(temp1);

                Node temp2 = new Node();
                Array.Copy(pCurrent.tiles, temp2.tiles, 9);
                temp2.tiles[8] = temp2.tiles[7];
                temp2.tiles[7] = 0;
                temp2.parent = pCurrent;
                output.Add(temp2);

            }
            return output;

        }

        double DistanceN(int[] first, int[] second)
        {
            var sum = first.Select((x, i) => (x - second[i]) * (x - second[i])).Sum();
            return Math.Sqrt(sum);
        }



    }
}
