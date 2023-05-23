using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleUnit : MonoBehaviour, INavUnit
{
    private bool canMove;
    [SerializeField] NavMeshAgent agent;

    public void MoveToDestination(Vector3 destination)
    {
        if (!canMove) return;
        agent.SetDestination(destination);
    }

    public void MoveToDestination(Hexagon hexagonDestination)
    {
        if(!canMove) return;

        agent.SetDestination(hexagonDestination.transform.position);
    }
}
