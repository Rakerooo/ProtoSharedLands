using Proto2.Input;
using Proto2.PathFinding;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Proto2.Map
{
    public class NewProtoCell : NewProtoPathPoint<NewProtoCell>, INewProtoHoverable, INewProtoInteractable
    {
        [SerializeField] private DecalProjector decalProjector;
        [SerializeField] private NewProtoBiotopes biotope;
        
        public NewProtoBiotopes Biotope => biotope;

        private NewProtoMap map;

        private bool hovered, selected;
        
        private void Start()
        {
            map = FindObjectOfType<NewProtoMap>();
            
            UpdateVisual();
        }

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
        
        private void UpdateSelected()
        {
            SetSelected(!selected);
            map.UpdateSelected(this);
        }
        private void UpdateVisual()
        {
            decalProjector.fadeFactor = selected ? map.CellSelectedOpacity : hovered ? map.CellHoveredOpacity : map.BaseCellOpacity;
        }
        
        #region Inputs
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
        #endregion
    }
}
