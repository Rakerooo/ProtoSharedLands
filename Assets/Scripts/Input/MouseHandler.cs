using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseHandler : MonoBehaviour
{
    private UnityEngine.Camera mainCamera;
    private IHoverable currentHover;

    private void Awake()
    {
        mainCamera = UnityEngine.Camera.main;
    }
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask: LayerMask.NameToLayer("Hoverable"));
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
        if (currentHover is ISelectable hover && !EventSystem.current.IsPointerOverGameObject())
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
