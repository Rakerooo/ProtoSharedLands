using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Proto2.PathFinding
{
    public static class NewProtoPathFinding<T> where T : NewProtoPathPoint<T>
    {
        private static readonly List<T> Path = new();

        public static void UpdatePathfinding<T1>(T1 baseCell, List<T1> cells) where T1 : T
        {
            DijkstraAlgorithm2(cells, baseCell);
        }

        // Function that implements Dijkstra's single source shortest path algorithm for a graph represented using neighbours representation 
        private static void DijkstraAlgorithm2<T1>(List<T1> cells, T1 baseCell) where T1 : T
        {
            // Initialize all distances as INFINITE and added[] as false
            foreach (var node in cells)
            {
                node.SetDistance(float.MaxValue);
                node.SetAdded(false);
            }
            
            // Distance of base node from itself is always 0 and it doesn't have a parent
            baseCell.SetDistance(0);
            baseCell.SetParent(null);

            // Find shortest path for all cells
            foreach (var unused in cells)
            {
                // Pick the minimum distance node from the set of vertices not yet processed. nearestCell is always equal to startCell in first iteration. 
                var nearestCell = baseCell;
                var shortestDistance = float.MaxValue;

                foreach (var secondaryCell in cells.Where(secondaryCell => !secondaryCell.Added && secondaryCell.Distance < shortestDistance))
                {
                    nearestCell = secondaryCell;
                    shortestDistance = secondaryCell.Distance;
                }

                // Mark the picked node as processed 
                nearestCell.SetAdded(true);

                // Update dist value of the adjacent cells of the picked node
                foreach (var neighbour in nearestCell.Neighbours)
                {
                    var edgeDistance = neighbour.MovementFactor / 2f + nearestCell.MovementFactor / 2f;

                    if (edgeDistance <= 0f || shortestDistance + edgeDistance >= neighbour.Distance) continue;
                    neighbour.SetParent(nearestCell);
                    neighbour.SetDistance(shortestDistance + edgeDistance);
                }
            }
        }
        
        public static List<T> GetFullPath<T1>(T1 baseCell, T1 targetedCell, IEnumerable<T1> cells) where T1 : T
        {
            foreach (var node in cells.Where(node => !node.Equals(baseCell) && node.Equals(targetedCell)))
            {
                Path.Clear();
                GetPath(node, Path);
                Path.Reverse();
                return Path;
            }

            return null;
        } 
        
        // A utility function to print the constructed distances array and shortest paths 
        public static void PrintDijkstra<T1>(T1 baseCell, IEnumerable<T1> cells) where T1 : T
        {
            Debug.Log("Vertex\tDistance\tPath");

            foreach (var node in cells.Where(node => !node.Equals(baseCell)))
            {
                Path.Clear();
                Path.Add(baseCell);
                Debug.Log($"{baseCell.gameObject.name}->{node.gameObject.name}\t{node.Distance}\t{string.Join(" ", GetPath(node, Path))}");
            }
        } 
 
        // Function to print shortest path from source to currentVertex using parents array 
        private static List<T1> GetPath<T1>(T1 node, List<T1> path) where T1 : NewProtoPathPoint<T1> 
        {
            // Base case : Source node has been processed 
            if (node == null) return path;
            path.Add(node);
            GetPath(node.Parent, path);
            return path;
        } 
        
        /*
        private static readonly List<NewProtoCell> UnvisitedCells = new();
        private static readonly List<NewProtoCell> FilteredNeighbours = new();
        
        private static void DijkstraAlgorithm1(IReadOnlyCollection<NewProtoCell> cells, NewProtoCell baseCell)
        {
            UnvisitedCells.Clear();
            UnvisitedCells.AddRange(cells);
            
            foreach (var node in cells.Where(node => !node.Equals(baseCell)))
            {
                node.SetDistance(float.MaxValue);
                node.ClearPositionsFromStart();
            }
            
            baseCell.SetDistance(0);
            baseCell.AddPositionFromStart(baseCell.Node.position);
            
            var currentCell = baseCell;
            
            while (currentCell != null)
            {
                UnvisitedCells.Remove(currentCell);
                FilteredNeighbours.Clear();

                var node = currentCell;
                foreach (var unvisitedCell in from unvisitedCell in UnvisitedCells from neighbour in node.Neighbours.Where(neighbour => unvisitedCell.Equals(neighbour.Cell)) select unvisitedCell)
                {
                    FilteredNeighbours.Add(unvisitedCell);
                }

                foreach (var neighbour in FilteredNeighbours)
                {
                    var newTheoreticDistance = currentCell.Distance + currentCell.MovementFactor / 2f + neighbour.MovementFactor / 2f;

                    if (!(newTheoreticDistance < neighbour.Distance)) continue;
                    neighbour.SetDistance(newTheoreticDistance);
                    neighbour.SetPositionsFromStart(currentCell.PositionsFromStart);
                    neighbour.AddPositionFromStart(neighbour.Node.position);
                }

                if (UnvisitedCells.Count == 0) break;

                currentCell = GetClosestCell(UnvisitedCells);
            }
        }
        
        private static void DijkstraAlgorithm(IEnumerable<NewProtoCell> cells, NewProtoCell baseCell)
        {
            foreach (var node in cells.Where(node => !node.Equals(baseCell)))
            {
                node.SetDistance(float.MaxValue);
            }
            
            baseCell.SetDistance(0);
            
            UnvisitedCells.Clear();
            UnvisitedCells.Add(baseCell);
            while (UnvisitedCells.Count > 0)
            {
                var currentNode = GetClosestCell(UnvisitedCells);
                UnvisitedCells.Remove(currentNode);

                foreach (var neighbour in currentNode.Neighbours)
                {
                    neighbour.SetDistance(currentNode.MovementFactor / 2f + neighbour.Cell.MovementFactor / 2f);
                    var newTheoreticDistance = currentNode.Distance + neighbour.Distance;

                    if (!(newTheoreticDistance < neighbour.Cell.Distance)) continue;
                    neighbour.Cell.SetDistance(newTheoreticDistance);
                    UnvisitedCells.Add(neighbour.Cell);
                }
            }
        }

        private static NewProtoCell GetClosestCell(IEnumerable<NewProtoCell> cells)
        {
            NewProtoCell closestCell = null;
            
            var minDistance = float.MaxValue;

            foreach (var node in cells.Where(node => node.Distance < minDistance))
            {
                closestCell = node;
                minDistance = node.Distance;
            }

            return closestCell;
        }*/
    }
}
