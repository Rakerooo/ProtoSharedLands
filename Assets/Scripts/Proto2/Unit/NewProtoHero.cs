using Proto2.Map;
using UnityEngine;

namespace Proto2.Unit
{
    public class NewProtoHero : NewProtoUnit<NewProtoCell>
    {
        [SerializeField] private MeshRenderer meshRenderer;

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
