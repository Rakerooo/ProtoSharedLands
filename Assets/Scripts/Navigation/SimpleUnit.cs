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

    [SerializeField] NavMeshAgent agent;

    [SerializeField] MeshRenderer rend;
    [SerializeField] Material selectedMat;
    [SerializeField] Material classicMat;
    [SerializeField] Material hoverMat;

    [ContextMenu("Debug Move")]
    public void DebugMove()
    {
        MoveToDestination(debugHex);
    }

    public void MoveToDestination(Vector3 destination)
    {
        if (!canMove) return;
        agent.SetDestination(destination);
    }

    public void MoveToDestination(Hexagon destination)
    {
        if (!canMove) return;
        agent.SetDestination(destination.transform.position);
    }

    public void OnDeselectItem()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverDisable()
    {
        rend.material = classicMat;
    }

    public void OnHoverEnable()
    {
        rend.material = hoverMat;
    }

    public void OnSelectItem()
    {
        rend.material = selectedMat;

    }
}
