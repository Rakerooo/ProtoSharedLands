using System.Collections.Generic;
using System.Linq;
using Proto2.PathFinding;
using Proto2.Unit;
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
        
        private NewProtoCell selectedCell;
        private NewProtoHero selectedHero;
        private List<NewProtoCell> cells;

        private void SetSelectedHero(NewProtoHero hero)
        {
            if (selectedHero != null) selectedHero.SetSelected(false);
            selectedHero = hero;

            if (selectedCell == null) return;
            DeselectedCell();
        }
        private void DeselectedHero()
        {
            selectedHero.SetSelected(false);
            selectedHero = null;
        }
        
        private void SetSelectedCell(NewProtoCell cell)
        {
            if (selectedCell != null) selectedCell.SetSelected(false);
            selectedCell = cell;
            EnableRegionUI(selectedCell);

            if (selectedHero == null) return;
            selectedHero.SetTarget(selectedCell);
            DeselectedHero();
        }
        private void DeselectedCell()
        {
            selectedCell.SetSelected(false);
            selectedCell = null;
            DisableRegionUI();
        }
        
        private void Start()
        {
            cells = FindObjectsOfType<NewProtoCell>().ToList();
            pathRenderer.SetLine(new List<Vector3>());
        }

        public void UpdateHeroSelected(NewProtoHero hero)
        {
            if (hero == null) return;
            if (selectedHero == null) SetSelectedHero(hero);
            else
            {
                if (hero.Equals(selectedHero)) DeselectedHero();
                else SetSelectedHero(hero);
            }
        }
        
        public void UpdateCellSelected(NewProtoCell cell)
        {
            if (cell == null) return;
            if (selectedCell == null) SetSelectedCell(cell);
            else
            {
                if (cell.Equals(selectedCell)) DeselectedCell();
                else SetSelectedCell(cell);
            }
        }

        private static void EnableRegionUI(NewProtoCell cell)
        {
            UIManager.instance.EnableRegionUI();
            UIManager.instance.SetCurrentSelectedCell(cell);
            cell.Region.UpdateResourceHandlerUI();
        }
        
        private static void DisableRegionUI()
        {
            UIManager.instance.DisableRegionUI();
            UIManager.instance.DeselectCell();
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