using System.Collections.Generic;
using UnityEngine;

namespace Proto2.Map
{
    public class NewProtoMap : MonoBehaviour
    {
        [SerializeField] private NewProtoPathRenderer pathRenderer;
        public int NbCells { get; private set; }
        private NewProtoCell selectedCell, secondarySelectedCell;

        private void Start()
        {
            var cells = FindObjectsOfType<NewProtoCell>();
            NbCells = cells.Length;
        }

        public void UpdateSelected(NewProtoCell cell)
        {
            if (cell == null)
            {
                pathRenderer.SetLine(new List<Vector3>());
                return;
            }
            if (cell.Equals(selectedCell)) selectedCell = null;
            else
            {
                if (selectedCell == null) selectedCell = cell;
                else
                {
                    if (secondarySelectedCell != null) secondarySelectedCell.SetSelected(false);
                    secondarySelectedCell = cell;
                    selectedCell.SetDistances();
                    pathRenderer.SetLine(selectedCell.GetPathToOtherCell(cell));
                }
            }
        }
    }
}