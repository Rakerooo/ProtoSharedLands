using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Proto2.Input
{
    public class NewProtoMouseHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask hoverableMask;
        [SerializeField] private Camera cam;

        private bool hasHover;
    
        private INewProtoHoverable currentHover;
        private GameObject lastGameObjectHovered;
    
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            var ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            Physics.Raycast(ray, out var hit, Mathf.Infinity, hoverableMask);
            if (hit.collider) {
                var hitGo = hit.collider.gameObject;
                if (lastGameObjectHovered is not null && hitGo.Equals(lastGameObjectHovered.gameObject)) return;
                lastGameObjectHovered = hitGo;
            
                var hoverableComponent = hit.collider.GetComponent<INewProtoHoverable>();
                if (hoverableComponent == null) return;
            
                if (currentHover == null) {
                    EnableHover(hoverableComponent);
                } else if (currentHover != hoverableComponent) {
                    DisableHover();
                    EnableHover(hoverableComponent);
                }
            } else {
                if (!hasHover) return;
                DisableHover();
                currentHover = null;
                lastGameObjectHovered = null;
            }
        }
    
        public void OnMainClick(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (currentHover is INewProtoInteractable interactable) {
                interactable.OnMainClick();
            }
        }

        private void EnableHover(INewProtoHoverable hoverable)
        {
            currentHover = hoverable;
            hasHover = true;
            currentHover?.OnHoverEnable();
        }

        private void DisableHover()
        {
            hasHover = false;
            currentHover?.OnHoverDisable();
        }
    }
}