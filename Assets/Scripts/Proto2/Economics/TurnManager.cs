using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    [SerializeField] private NewProto_UITopBarController _UIController;
    [SerializeField] private ushort currentTurn = 0;
    [SerializeField] private UnityEvent startPlayerTurnEvent;
    [SerializeField] private UnityEvent endPlayerTurnEvent;
    [SerializeField] private UnityEvent startTitanTurnEvent;
    [SerializeField] private UnityEvent endTitanTurnEvent;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        startPlayerTurnEvent.AddListener(() =>
        {
            Debug.Log("Start player turn");
        });
        endPlayerTurnEvent.AddListener(() =>
        {
            Debug.Log("End player turn");
        });
        startTitanTurnEvent.AddListener(() =>
        {
            Debug.Log("Start titan turn");
        });
        endTitanTurnEvent.AddListener(() =>
        {
            Debug.Log("End titan turn");
            currentTurn++;
            UpdateUI();
        });
    }

    private void Start()
    {
        UpdateUI();
    }

    public UnityEvent GetStartPlayerTurnEvent()
    {
        return startPlayerTurnEvent;
    }
    public UnityEvent GetEndPlayerTurnEvent()
    {
        return endPlayerTurnEvent;
    }
    public UnityEvent GetStartTitanTurnEvent()
    {
        return startTitanTurnEvent;
    }
    public UnityEvent GetEndTitanTurnEvent()
    {
        return endTitanTurnEvent;
    }
    
    [ContextMenu("Start Turn")]
    public void StartPlayerTurn()
    {
        startPlayerTurnEvent.Invoke();
    }
    public void EndPlayerTurn()
    {
        endPlayerTurnEvent.Invoke();
    }
    public void StartTitanTurn()
    {
        startTitanTurnEvent.Invoke();
    }
    public void EndTitanTurn()
    {
        endTitanTurnEvent.Invoke();
    }

    public void UpdateUI()
    {
        _UIController.UpdateTurnCount(currentTurn);
    }
    
}
