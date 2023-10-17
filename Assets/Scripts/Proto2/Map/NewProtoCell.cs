using System.Collections.Generic;
using Proto2.Input;
using UnityEngine;

namespace Proto2.Map
{
    public class NewProtoCell : MonoBehaviour, INewProtoHoverable, INewProtoInteractable
    {
        [SerializeField] private List<NewProtoCell> neighbours;
        [SerializeField] private Transform node;
        [SerializeField] private NewProtoBiotopes biotope;

        private bool hovered, selected;
        
        private void SetHover(bool isHovered)
        {
            hovered = isHovered;
            Debug.Log($"Is {gameObject.name} hovered : {hovered}");
        }
        private void UpdateSelected()
        {
            selected = !selected;
            Debug.Log($"Is {gameObject.name} selected : {selected}");
        }

        private void UpdateVisual()
        {
            if (selected)
            {
                // Set selected view
            }
            else
            {
                if (hovered)
                {
                    // Set hovered view
                }
                else
                {
                    // Set base view
                }
            }
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
