using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TitanMove : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] UnityEvent onEndMove;

    [Header("ColorChange")]
    [SerializeField] Material originalMat;
    [SerializeField] Material changedMat;
    [SerializeField] Renderer rend;

    private bool colorSwap = false;

    [ContextMenu("Move")]
    public void MoveTitan()
    {
        animator.SetTrigger("Move");
    }

    //Appelé dans l'animation du Titan
    public void TitanEndMove()
    {
        onEndMove?.Invoke();
    }

    public void SwapColor()
    {
        colorSwap = !colorSwap;
        rend.material = colorSwap ? changedMat : originalMat;

    }
}
