using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestIHoverable : MonoBehaviour, IHoverable, ISelectable
{
    public MeshRenderer mr;
    public Material defaultMaterial;
    public Material hoverMaterial;
    public Material selectMaterial;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material = defaultMaterial;
    }

    public void OnHoverEnable()
    {
        mr.material = hoverMaterial;
    }

    public void OnHoverDisable()
    {
        mr.material = defaultMaterial;
    }
    
    public void OnSelectItem()
    {
        mr.material = selectMaterial;
    }
}
