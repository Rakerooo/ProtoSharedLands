using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleUnit : MonoBehaviour
{
    private bool canMove;
    [SerializeField] NavMeshAgent agent;

    public void MoveToDestination(Vector3 destination)
    {
        if (!canMove) return;
    }
}
