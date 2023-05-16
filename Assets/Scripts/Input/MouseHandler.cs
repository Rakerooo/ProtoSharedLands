using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseHandler : MonoBehaviour
{
    private InputAction _inputs;
    private UnityEngine.Camera mainCamera;
    [SerializeField] private IHoverable currentHover;

    private void Awake()
    {
        mainCamera = UnityEngine.Camera.main;
    }

    private void Start()
    {
        _inputs = new InputAction();
        _inputs.Enable();
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
                if (currentHover == null && tmp != null)
                {
                    currentHover = tmp;
                    currentHover.OnHoverEnable();

                }
                else if (currentHover != tmp && tmp != null)
                {
                    currentHover.OnHoverDisable();
                    currentHover = tmp;
                    currentHover.OnHoverEnable();
                }
            }
            else // TODO: Check if selected object is still selected by the player (by listening to an event ?)
            {
                currentHover?.OnHoverDisable();
                currentHover = null;
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
    
}
