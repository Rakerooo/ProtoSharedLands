using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[CreateAssetMenu(order = 1,menuName = "NewQuest", fileName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private Canvas questUI; //TODO : changer en UIDocument si on utilise l'UI toolkit
    [SerializeField] private UnityEvent onCompletionEvent;
    [SerializeField] private byte startTurn;
    [SerializeField] private byte maxTurnBeforeFailure;
    
}
