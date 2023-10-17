using System.Collections.Generic;
using System.Linq;

namespace Proto2.Map
{
    public static class NewProtoPathFinding
    {
        private static readonly List<NewProtoCell> UnvisitedCells = new();
        private static readonly List<NewProtoCell> FilteredNeighbours = new();

        public static void UpdatePathfinding(this NewProtoCell baseCell, List<NewProtoCell> cells)
        {
            DijkstraAlgorithm1(cells, baseCell);
        }
        
        //https://www.geeksforgeeks.org/printing-paths-dijkstras-shortest-path-algorithm/
        
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
        }
    }
}
