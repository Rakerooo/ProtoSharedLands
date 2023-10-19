using System.Collections.Generic;
using System.Linq;
using Proto2.Map;
using Proto2.PathFinding;
using UnityEngine;

namespace Proto2.Unit
{
    public class NewProtoHero : NewProtoUnit<NewProtoCell>
    {
        [SerializeField] private MeshRenderer meshRenderer;
        
        private bool startTitanTurn, hasMoved;
        private NewProtoMap map;
        private Material material;
        private bool hovered, selected;
        
        private void SetHover(bool isHovered)
        {
            hovered = isHovered;
            UpdateVisual();
        }
        public void SetSelected(bool isSelected)
        {
            selected = isSelected;
            UpdateVisual();
        }
        public void SetTarget(NewProtoCell cell)
        {
            finalTargetPos = cell;
            path.Clear();
            path.AddRange(NewProtoPathFinding<NewProtoCell>.GetFullPath(currentPos, finalTargetPos, possiblePositions, true));
            pathRenderer.SetLine(path.Select(c => c.Node.position).ToList());
            pathIndex = 0;
            pathIndex++;
            
            if (hasMoved) return;
            hasMoved = true;
            UpdateTargetPos();
            StartCoroutine(MoveToTargetPos());
        }
        
        private void UpdateTargetPos()
        {
            if (currentPos == null) return;
            if (finalTargetPos == null || finalTargetPos.Equals(currentPos) || pathIndex >= path.Count) return;
            targetPos = path[pathIndex];
            pathIndex++;
        }
        
        private new void Start()
        {
            base.Start();
            
            map = FindObjectOfType<NewProtoMap>();
            
            var thisMaterial = meshRenderer.material;
            material = thisMaterial;
            
            UpdateVisual();
        }
        
        private void UpdateSelected()
        {
            SetSelected(!selected);
            map.UpdateHeroSelected(this);
        }
        private void UpdateVisual()
        {
            if (selected) material.color = Color.red;
            else material.color = hovered ? Color.yellow : Color.green;
        }

        public void StartTurn()
        {
            if (hasMoved)
            {
                hasMoved = false;
                startTitanTurn = true;
                MovementFinished();
            }
            else
            {
                UpdateTargetPos();
                StartCoroutine(MoveToTargetPos());
                startTitanTurn = true;
            }
        }
        protected override void MovementFinished()
        {
            if (startTitanTurn)
            {
                TurnManager.instance.StartTitanTurn();
                startTitanTurn = false;
            }
            if (finalTargetPos == currentPos) pathRenderer.SetLine(new List<Vector3>());
        }
        
        public void OnHoverEnable()
        {
            SetHover(true);
        }
        public void OnHoverDisable()
        {
            SetHover(false);
        }
        public void OnMainClick()
        {
            UpdateSelected();
        }
    }
}
