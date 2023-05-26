using MapScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleUnit : MonoBehaviour, INavUnit, ISelectable, IHoverable
{
    [Header("Debug")]
    [SerializeField] Hexagon debugHex; 
    [SerializeField] private bool canMove = true;
    [SerializeField] UnitNavigationService unitNavigationService;

    [Space(10f)]
    [SerializeField] NavMeshAgent agent;

    [SerializeField] MeshRenderer rend;
    [SerializeField] Material selectedMat;
    [SerializeField] Material classicMat;
    [SerializeField] Material hoverMat;

    private bool isSelected = false;

    [ContextMenu("Debug Move")]
    public void DebugMove()
    {
        MoveToDestination(debugHex);
    }

    public void MoveToDestination(Vector3 destination)
    {
        if (!canMove) return;
        agent.SetDestination(destination);
        rend.material = classicMat;
    }

    public void MoveToDestination(Hexagon destination)
    {
        if (!canMove) return;
        agent.SetDestination(destination.transform.position);
    }

    public void OnDeselectItem()
    {
        //throw new System.NotImplementedException();
        Debug.Log($"{this.gameObject.name} On me déselectionne !");
        rend.material = classicMat;
        isSelected = false;
    }

    public void OnAlternateSelect()
    {
        //Do nothing here  
    }

    public void OnAlternateDeselect()
    {
        //throw new System.NotImplementedException();
    }

    public void OnHoverDisable()
    {
        if(!isSelected) rend.material = classicMat;
    }

    public void OnHoverEnable()
    {
        if(!isSelected) rend.material = hoverMat;
    }

    public void OnSelectItem()
    {
        Debug.Log($"{this.gameObject.name} On me sélectionne !");
        rend.material = selectedMat;
        unitNavigationService.SelectUnit(this);

        isSelected = true;
    }
}
