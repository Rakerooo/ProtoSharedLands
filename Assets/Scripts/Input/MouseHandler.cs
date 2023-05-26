using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseHandler : MonoBehaviour
{
    [SerializeField] private UnityEngine.Camera cam;
    private IHoverable currentHover;
    
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            var ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            Physics.Raycast(ray, out var hit, Mathf.Infinity, GameManager.instance.Layers.hoverableMask);
            if (hit.collider)
            {
                IHoverable tmp = hit.collider.GetComponent<IHoverable>();
                if (tmp != null)
                {
                    if (currentHover == null)
                    {
                        UpdateCurrentHover(tmp);
                        EnableHover();

                    }
                    else if (currentHover != tmp)
                    {
                        DisableHover();
                        UpdateCurrentHover(tmp);
                        EnableHover();
                    }
                }
            }
            else
            {
                DisableHover();
                UpdateCurrentHover(null);
            }
        }
    }
    
    
    public void OnSelect()
    {
        if (currentHover is ISelectable hover && !EventSystem.current.IsPointerOverGameObject() && GameManager.instance.GetCanPlay())
        {
            hover.OnSelectItem();
        }
    }

    private void UpdateCurrentHover(IHoverable newItem)
    {
        currentHover = newItem;
    }

    private void DisableHover()
    {
        currentHover?.OnHoverDisable();
    }

    private void EnableHover()
    {
        currentHover?.OnHoverEnable();
    }
    
}
