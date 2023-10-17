using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Proto2.Map
{
    public class NewProtoMap : MonoBehaviour
    {
        [SerializeField] private NewProtoPathRenderer pathRenderer;
        public int NbCells { get; private set; }
        private NewProtoCell selectedCell, secondarySelectedCell;
        private List<NewProtoCell> cells;

        private void Start()
        {
            cells = FindObjectsOfType<NewProtoCell>().ToList();
            NbCells = cells.Count;
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
                    selectedCell.SetDistances();
                    DebugDistances();
                }
                else
                {
                    ResetSecondaryCell();
                    secondarySelectedCell = cell;
                    //pathRenderer.SetLine(selectedCell.GetPathToOtherCell(cell));
                }
            }
        }

        private void DebugDistances()
        {
            foreach (var cell in cells)
            {
                Debug.Log($"{cell.gameObject.name} : {cell.Distance}");
            }
        }

    }
}