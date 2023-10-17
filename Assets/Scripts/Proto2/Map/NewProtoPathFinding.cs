using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Proto2.Map
{
    public static class NewProtoPathFinding
    {
        private static readonly List<NewProtoCell> UnvisitedNodes = new();
        private static readonly List<Vector3> NodesToVisit = new();
        
        public static List<Vector3> GetPathToOtherCell(this NewProtoCell baseCell, NewProtoCell targetedCell, IEnumerable<NewProtoCell> cells)
        {
            return DijkstraAlgorithmPoints(cells, baseCell, targetedCell);
        }

        public static void SetDistances(this NewProtoCell baseCell, IEnumerable<NewProtoCell> cells)
        {
            DijkstraAlgorithmDistance(cells, baseCell);
        }
        
        private static List<Vector3> DijkstraAlgorithmPoints(IEnumerable<NewProtoCell> cells, NewProtoCell baseCell, NewProtoCell targetedCell)
        {
            
            return NodesToVisit;
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
                var currentNode = GetClosestNode(UnvisitedNodes);
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

        private static NewProtoCell GetClosestNode(IEnumerable<NewProtoCell> cells)
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
