using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace WaterJugProblem.handler
{
    public class HandlerWaterJugProblem
    {
        public class WaterJugRespone
        {
            public int jugX;
            public int jugY;
            public string explanation { get; set; }

        }
        public static void bfsSolution(Dictionary<string, int[]> nodePath, int[] u, List<string> solution, string[,] movement)
        {
            if (u[0] == 0 && u[1] == 0)
            {
                return;
            }
            bfsSolution(nodePath, nodePath[$"{u[0]},{u[1]}"], solution, movement);

            WaterJugRespone response = new WaterJugRespone
            {
                jugX = u[0],
                jugY = u[1],
                explanation = movement[u[0], u[1]],
            };
            string json = JsonConvert.SerializeObject(response);
            solution.Add(json);
        }
        public static List<string> SolveWaterBucketProblem(int bucketX, int bucketY, int target)
        {
            List<string> solution = new List<string>();

            int[,] nodeMap = new int[bucketX + 1, bucketY + 1];
            string[,] movement = new string[bucketX + 1, bucketY + 1];
            Dictionary<string, int[]> nodePath = new Dictionary<string, int[]>();
            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { 0, 0 });
            int[] u = new int[] { 0, 0 };
            bool isSolvable = false;

            while (queue.Count > 0)
            {
                u = queue.Dequeue();


                if (u[0] == target || u[1] == target)
                {

                    bfsSolution(nodePath, u, solution, movement);

                    isSolvable = true;
                    break;
                }



                // Llena el cubo Y
                if (nodeMap[u[0], 0] != 1)
                {
                    queue.Enqueue(new int[] { u[0], bucketY });
                    nodePath.Add($"{u[0]},{bucketY}", new int[] { u[0], u[1] });
                    nodeMap[u[0], bucketY] = 1;

                    movement[u[0], bucketY] = "Fill bucket y";
                }
                // Llena el cubo X
                if (nodeMap[bucketX, u[1]] != 1)
                {
                    queue.Enqueue(new int[] { bucketX, u[1] });
                    nodePath.Add($"{bucketX},{u[1]}", new int[] { u[0], u[1] });
                    nodeMap[bucketX, u[1]] = 1;
                    movement[bucketX, u[1]] = "Fill bucket x";
                }

                // Transfiere agua del cubo X al cubo Y
                int d = bucketY - u[1];
                if (u[0] >= d)
                {
                    int c = u[0] - d;
                    if (nodeMap[c, bucketY] != 1)
                    {
                        queue.Enqueue(new int[] { c, bucketY });
                        nodePath.Add($"{c},{bucketY}", new int[] { u[0], u[1] });
                        nodeMap[c, bucketY] = 1;
                        movement[c, bucketY] = "Transfer bucket x to bucket y";
                    }
                }
                else
                {
                    int c = u[0] + u[1];
                    if (nodeMap[0, c] != 1)
                    {
                        queue.Enqueue(new int[] { 0, c });
                        nodePath.Add($"{0},{c}", new int[] { u[0], u[1] });
                        nodeMap[0, c] = 1;
                        movement[0, c] = "Transfer bucket x to bucket y";
                    }
                }

                // Transfiere agua del cubo Y al cubo X
                d = bucketX - u[0];
                if (u[1] >= d)
                {
                    int c = u[1] - d;
                    if (nodeMap[bucketX, c] != 1)
                    {
                        queue.Enqueue(new int[] { bucketX, c });
                        nodePath.Add($"{bucketX},{c}", new int[] { u[0], u[1] });
                        nodeMap[bucketX, c] = 1;
                        movement[bucketX, c] = "Transfer bucket y to bucket x";
                    }
                }
                else
                {
                    int c = u[0] + u[1];
                    if (nodeMap[c, 0] != 1)
                    {
                        queue.Enqueue(new int[] { c, 0 });
                        nodePath.Add($"{c},{0}", new int[] { u[0], u[1] });
                        nodeMap[c, 0] = 1;
                        movement[c, 0] = "Transfer bucket y to bucket x";
                    }
                }

                // Vacia el cubo Y
                if (nodeMap[u[0], 0] != 1)
                {
                    queue.Enqueue(new int[] { u[0], 0 });
                    nodePath.Add($"{u[0]},{0}", new int[] { u[0], u[1] });
                    nodeMap[u[0], 0] = 1;
                    movement[u[0], 0] = "Dump bucket y";
                }

                // Vacia el cubo X
                if (nodeMap[0, u[1]] != 1)
                {
                    queue.Enqueue(new int[] { 0, u[1] });
                    nodePath.Add($"{0},{u[1]}", new int[] { u[0], u[1] });
                    nodeMap[0, u[1]] = 1;
                    movement[0, u[1]] = "Dump bucket x";
                }
            }

            if (!isSolvable)
            {
                solution.Add("Solution not possible");
            }


            return solution;
        }
    }
}
