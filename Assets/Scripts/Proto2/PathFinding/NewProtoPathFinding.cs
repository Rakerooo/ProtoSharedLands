using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Proto2.Map
{
    public static class NewProtoPathFinding
    {
        private static readonly List<NewProtoCell> Path = new();

        public static void UpdatePathfinding(this NewProtoCell baseCell, List<NewProtoCell> cells)
        {
            DijkstraAlgorithm2(cells, baseCell);
        }

        // Function that implements Dijkstra's single source shortest path algorithm for a graph represented using neighbours representation 
        private static void DijkstraAlgorithm2(List<NewProtoCell> cells, NewProtoCell baseCell) 
        {
            // Initialize all distances as INFINITE and added[] as false
            foreach (var cell in cells)
            {
                cell.SetDistance(float.MaxValue);
                cell.SetAdded(false);
            }
            
            // Distance of base cell from itself is always 0 and it doesn't have a parent
            baseCell.SetDistance(0);
            baseCell.SetParent(null);

            // Find shortest path for all cells
            foreach (var unused in cells)
            {
                // Pick the minimum distance cell from the set of vertices not yet processed. nearestCell is always equal to startCell in first iteration. 
                var nearestCell = baseCell;
                var shortestDistance = float.MaxValue;

                foreach (var secondaryCell in cells.Where(secondaryCell => !secondaryCell.Added && secondaryCell.Distance < shortestDistance))
                {
                    nearestCell = secondaryCell;
                    shortestDistance = secondaryCell.Distance;
                }

                // Mark the picked cell as processed 
                nearestCell.SetAdded(true);

                // Update dist value of the adjacent cells of the picked cell
                foreach (var neighbour in nearestCell.Neighbours)
                {
                    var edgeDistance = neighbour.MovementFactor / 2f + nearestCell.MovementFactor / 2f;

                    if (edgeDistance <= 0f || shortestDistance + edgeDistance >= neighbour.Distance) continue;
                    neighbour.SetParent(nearestCell);
                    neighbour.SetDistance(shortestDistance + edgeDistance);
                }
            }
        }
        
        public static List<NewProtoCell> GetFullPath(this NewProtoCell baseCell, NewProtoCell targetedCell, IEnumerable<NewProtoCell> cells)
        {
            foreach (var cell in cells.Where(cell => !cell.Equals(baseCell) && cell.Equals(targetedCell)))
            {
                Path.Clear();
                GetPath(cell, Path);
                Path.Reverse();
                return Path;
            }

            return null;
        } 
        
        // A utility function to print the constructed distances array and shortest paths 
        public static void PrintDijkstra(this NewProtoCell baseCell, IEnumerable<NewProtoCell> cells)
        {
            Debug.Log("Vertex\tDistance\tPath");

            foreach (var cell in cells.Where(cell => !cell.Equals(baseCell)))
            {
                Path.Clear();
                Path.Add(baseCell);
                Debug.Log($"{baseCell.gameObject.name}->{cell.gameObject.name}\t{cell.Distance}\t{string.Join(" ", GetPath(cell, Path))}");
            }
        } 
 
        // Function to print shortest path from source to currentVertex using parents array 
        private static List<NewProtoCell> GetPath(NewProtoCell cell, List<NewProtoCell> path) 
        {
            // Base case : Source node has been processed 
            if (cell == null) return path;
            path.Add(cell);
            GetPath(cell.Parent, path);
            return path;
        } 
        
        /*
        private static readonly List<NewProtoCell> UnvisitedCells = new();
        private static readonly List<NewProtoCell> FilteredNeighbours = new();
        
        private static void DijkstraAlgorithm1(IReadOnlyCollection<NewProtoCell> cells, NewProtoCell baseCell)
        {
            UnvisitedCells.Clear();
            UnvisitedCells.AddRange(cells);
            
            foreach (var cell in cells.Where(cell => !cell.Equals(baseCell)))
            {
                cell.SetDistance(float.MaxValue);
                cell.ClearPositionsFromStart();
            }
            
            baseCell.SetDistance(0);
            baseCell.AddPositionFromStart(baseCell.Node.position);
            
            var currentCell = baseCell;
            
            while (currentCell != null)
            {
                UnvisitedCells.Remove(currentCell);
                FilteredNeighbours.Clear();

                var cell = currentCell;
                foreach (var unvisitedCell in from unvisitedCell in UnvisitedCells from neighbour in cell.Neighbours.Where(neighbour => unvisitedCell.Equals(neighbour.Cell)) select unvisitedCell)
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
            foreach (var cell in cells.Where(cell => !cell.Equals(baseCell)))
            {
                cell.SetDistance(float.MaxValue);
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

            foreach (var cell in cells.Where(cell => cell.Distance < minDistance))
            {
                closestCell = cell;
                minDistance = cell.Distance;
            }

            return closestCell;
        }*/
    }
}
