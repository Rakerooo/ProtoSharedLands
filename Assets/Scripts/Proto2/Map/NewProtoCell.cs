using System;
using System.Collections.Generic;
using Proto2.Input;
using UnityEngine;

namespace Proto2.Map
{
    public class NewProtoCell : MonoBehaviour, INewProtoHoverable, INewProtoInteractable
    {
        [SerializeField] private MeshRenderer meshRenderer;
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
        private Material material;

        private bool hovered, selected;
        
        private void Start()
        {
            var thisMaterial = meshRenderer.material;
            material = thisMaterial;
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
            map.UpdateSelected(selected ? this : null);
        }
        private void UpdateVisual()
        {
            if (selected) material.color = Color.red;
            else material.color = hovered ? Color.yellow : Color.green;
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
