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
            Debug.Log(cells.Count);
            pathRenderer.SetLine(new List<Vector3>());
        }
        
        private void ResetSecondaryCell()
        {
            if (secondarySelectedCell != null) secondarySelectedCell.SetSelected(false);
            secondarySelectedCell = null;
        }

        public void UpdateSelected(NewProtoCell cell)
        {
            if (cell == null || cell.Equals(selectedCell))
            {
                pathRenderer.SetLine(new List<Vector3>());
                selectedCell = null;
                ResetSecondaryCell();
            }
            else
            {
                if (selectedCell == null)
                {
                    selectedCell = cell;
                    selectedCell.UpdatePathfinding(cells);
                }
                else
                {
                    ResetSecondaryCell();
                    secondarySelectedCell = cell;
                    selectedCell.PrintDijkstra(cells);
                    pathRenderer.SetLine(selectedCell.GetFullPath(secondarySelectedCell, cells).Select(c => c.Node.position).ToList());
                }
            }
        }
    }
}