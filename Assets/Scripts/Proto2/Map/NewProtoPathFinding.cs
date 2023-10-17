using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Proto2.Map
{
    public static class NewProtoPathFinding
    {
        private static readonly List<NewProtoCell> UnvisitedNodes = new();
        private static readonly List<NewProtoCell> AllCells = new();

        public static void UpdatePathfinding(this NewProtoCell baseCell, IEnumerable<NewProtoCell> cells)
        {
            DijkstraAlgorithmPoints(cells, baseCell);
        }
        
        private static void DijkstraAlgorithmPoints(IEnumerable<NewProtoCell> cells, NewProtoCell baseCell)
        {
            foreach (var cell in cells.Where(cell => !cell.Equals(baseCell)))
            {
                cell.SetDistance(float.MaxValue);
                cell.ClearPositionsFromStart();
            }
            
            UnvisitedNodes.Clear();
            UnvisitedNodes.Add(baseCell);
            
            baseCell.SetDistance(0);
            baseCell.AddPositionFromStart(baseCell.Node.position);
            
            while (UnvisitedNodes.Count > 0)
            {
                var currentCell = GetClosestCell(UnvisitedNodes);
                UnvisitedNodes.Remove(currentCell);

                foreach (var neighbour in currentCell.Neighbours)
                {
                    neighbour.SetDistance(currentCell.MovementFactor / 2f + neighbour.Cell.MovementFactor / 2f);
                    var newTheoreticDistance = currentCell.Distance + neighbour.Distance;

                    if (!(newTheoreticDistance < neighbour.Cell.Distance)) continue;
                    neighbour.Cell.SetDistance(newTheoreticDistance);
                    UnvisitedNodes.Add(neighbour.Cell);
                    neighbour.Cell.SetPositionsFromStart(currentCell.PositionsFromStart);
                    neighbour.Cell.AddPositionFromStart(neighbour.Cell.Node.position);
                }
            }
        }
        
        private static void DijkstraAlgorithmDistance(IEnumerable<NewProtoCell> cells, NewProtoCell baseCell)
        {
            foreach (var cell in cells.Where(cell => !cell.Equals(baseCell)))
            {
                cell.SetDistance(float.MaxValue);
            }
            
            baseCell.SetDistance(0);
            
            UnvisitedNodes.Clear();
            UnvisitedNodes.Add(baseCell);
            while (UnvisitedNodes.Count > 0)
            {
                var currentNode = GetClosestCell(UnvisitedNodes);
                UnvisitedNodes.Remove(currentNode);

                foreach (var neighbour in currentNode.Neighbours)
                {
                    neighbour.SetDistance(currentNode.MovementFactor / 2f + neighbour.Cell.MovementFactor / 2f);
                    var newTheoreticDistance = currentNode.Distance + neighbour.Distance;

                    if (!(newTheoreticDistance < neighbour.Cell.Distance)) continue;
                    neighbour.Cell.SetDistance(newTheoreticDistance);
                    UnvisitedNodes.Add(neighbour.Cell);
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
        }
    }
}
