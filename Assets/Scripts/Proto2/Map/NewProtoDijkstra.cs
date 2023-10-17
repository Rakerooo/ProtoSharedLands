using System.Collections.Generic;
using UnityEngine;

namespace Proto2.Map
{
    public class NewProtoDijkstra : MonoBehaviour
    {
        // A utility function to find the vertex with minimum distance value, from the set of vertices not yet included in shortest path tree
        private static int MinDistance(IReadOnlyList<int> dist, IReadOnlyList<bool> sptSet, int nbNode)
        {
            // Initialize min value
            int min = int.MaxValue, minIndex = -1;
     
            for (var v = 0; v < nbNode; v++)
                if (sptSet[v] == false && dist[v] <= min) {
                    min = dist[v];
                    minIndex = v;
                }
     
            return minIndex;
        }
     
        // A utility function to print the constructed distance array
        private static void PrintSolution(IReadOnlyList<int> dist, int nbNode)
        {
            Debug.Log("Vertex Distance " + "from Source");
            for (var i = 0; i < nbNode; i++)
                Debug.Log(i + " \t\t " + dist[i] + "\n");
        }
        
        // Function that implements Dijkstra's single source shortest path algorithm for a graph represented using adjacency matrix representation
        public static void Dijkstra(int[, ] graph, int src, int nbNode)
        {
            var dist = new int[nbNode]; // The output array. dist[i] will hold the shortest distance from src to i
     
            // sptSet[i] will be true if vertex i is included in shortest path tree or shortest distance from src to i is finalized
            var sptSet = new bool[nbNode];
     
            // Initialize all distances as INFINITE and stpSet[] as false
            for (var i = 0; i < nbNode; i++) {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }
     
            // Distance of source vertex from itself is always 0
            dist[src] = 0;
     
            // Find shortest path for all vertices
            for (var count = 0; count < nbNode - 1; count++) {
                // Pick the minimum distance vertex from the set of vertices not yet processed. u is always equal to src in first iteration.
                var u = MinDistance(dist, sptSet, nbNode);
     
                // Mark the picked vertex as processed
                sptSet[u] = true;
     
                // Update dist value of the adjacent vertices of the picked vertex.
                for (var v = 0; v < nbNode; v++)
                {
                    // Update dist[v] only if is not in sptSet, there is an edge from u to v,
                    // and total weight of path from src to v through u is smaller than current value of dist[v]
                    if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
                }
            }
     
            // print the constructed distance array
            PrintSolution(dist, nbNode);
        }
    }
}
