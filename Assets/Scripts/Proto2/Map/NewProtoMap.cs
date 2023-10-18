using System.Collections.Generic;
using System.Linq;
using Proto2.PathFinding;
using UnityEngine;

namespace Proto2.Map
{
    public class NewProtoMap : MonoBehaviour
    {
        [SerializeField] private NewProtoPathRenderer pathRenderer;
        [SerializeField] private float cellSelectedOpacity = 1f, cellHoveredOpacity = 0.3f, baseCellOpacity = 0.1f;

        public float CellSelectedOpacity => cellSelectedOpacity;
        public float CellHoveredOpacity=> cellHoveredOpacity;
        public float BaseCellOpacity => baseCellOpacity;
        
        private NewProtoCell selectedCell, secondarySelectedCell;
        private List<NewProtoCell> cells;

        private void Start()
        {
            cells = FindObjectsOfType<NewProtoCell>().ToList();
            pathRenderer.SetLine(new List<Vector3>());
        }

        public void UpdateSelected(NewProtoCell cell)
        {
            if (cell == null) return;
            if (selectedCell == null)
            {
                selectedCell = cell;
                //selectedCell.PrintDijkstra(cells);
                NewProtoPathFinding<NewProtoCell>.UpdatePathfinding(selectedCell, cells);
            }
            else
            {
                if (cell.Equals(selectedCell))
                {
                    selectedCell = null;
                    pathRenderer.SetLine(new List<Vector3>());
                }
                else
                {
                    selectedCell = cell;
                    selectedCell.SetSelected(false);
                }
            }
        }
        
        /*private void SetSecondaryCell(NewProtoCell cell)
        {
            secondarySelectedCell = cell;
            pathRenderer.SetLine(NewProtoPathFinding<NewProtoCell>.GetFullPath(selectedCell, secondarySelectedCell, cells).Select(c => c.Node.position).ToList());
        }
        private void ResetSecondaryCell()
        {
            if (secondarySelectedCell != null) secondarySelectedCell.SetSelected(false);
            secondarySelectedCell = null;
        }
        public void UpdateSelectedTwo(NewProtoCell cell)
        {
            if (cell == null) return;
            if (selectedCell == null)
            {
                selectedCell = cell;
                //selectedCell.PrintDijkstra(cells);
                NewProtoPathFinding<NewProtoCell>.UpdatePathfinding(selectedCell, cells);
            }
            else
            {
                if (cell.Equals(selectedCell))
                {
                    selectedCell = null;
                    pathRenderer.SetLine(new List<Vector3>());
                    ResetSecondaryCell();
                }
                else if (secondarySelectedCell == null) SetSecondaryCell(cell);
                else if (cell.Equals(secondarySelectedCell))
                {
                    pathRenderer.SetLine(new List<Vector3>());
                    ResetSecondaryCell();
                }
                else
                {
                    ResetSecondaryCell();
                    SetSecondaryCell(cell);
                }
            }
        }*/
    }
}