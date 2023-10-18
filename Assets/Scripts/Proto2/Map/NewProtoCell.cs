using System.Collections.Generic;
using Proto2.Input;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Proto2.Map
{
    public class NewProtoCell : MonoBehaviour, INewProtoHoverable, INewProtoInteractable
    {
        [SerializeField] private DecalProjector decalProjector;
        [SerializeField] private List<NewProtoCell> neighbours;
        [SerializeField] private Transform node;
        [SerializeField] private NewProtoBiotopes biotope;
        [SerializeField] private float movementFactor = 1f;

        public List<NewProtoCell> Neighbours => neighbours;
        public Transform Node => node;
        public NewProtoBiotopes Biotope => biotope;
        public float MovementFactor => movementFactor;
        public float Distance { get; private set; }
        public bool Added { get; private set; }
        public NewProtoCell Parent { get; private set; }

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
        public void SetDistance(float newDistance)
        {
            Distance = newDistance;
        }
        public void SetAdded(bool added)
        {
            Added = added;
        }
        public void SetParent(NewProtoCell parent)
        {
            Parent = parent;
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
