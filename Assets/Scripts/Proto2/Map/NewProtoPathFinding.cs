using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Proto2.Map
{
    public static class NewProtoPathFinding
    {
        public static List<Vector3> GetPathToOtherCell(this NewProtoCell baseCell, NewProtoCell targetedCell)
        {
            return new List<Vector3>();
        }

        public static void SetDistances(this NewProtoCell baseCell)
        {
            DijkstraAlgorithm(baseCell);
        }
        
        private static void DijkstraAlgorithm(NewProtoCell baseCell)
        {
            baseCell.SetDistance(0);
            
            var unvisitedNodes = new List<NewProtoCell> { baseCell };
            while (unvisitedNodes.Count > 0)
            {
                var currentNode = GetClosestNode(unvisitedNodes);
                unvisitedNodes.Remove(currentNode);

                foreach (var neighbour in currentNode.Neighbours)
                {
                    var distanceBetween = currentNode.Distance + neighbour.Cell.MovementFactor;

                    if (!(distanceBetween < neighbour.Distance)) continue;
                    neighbour.SetDistance(distanceBetween);
                    unvisitedNodes.Add(neighbour.Cell);
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
