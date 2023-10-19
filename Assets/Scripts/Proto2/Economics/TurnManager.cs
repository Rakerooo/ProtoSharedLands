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

    [SerializeField] private bool isPlayerTurn = true;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        startPlayerTurnEvent.AddListener(() =>
        {
            Debug.Log("Starting player turn");
            isPlayerTurn = true;
            UIManager.instance.EnableEndTurnButton();
        });
        endPlayerTurnEvent.AddListener(() =>
        {
            Debug.Log("Ending player turn");
            isPlayerTurn = false;
            UIManager.instance.DisableEndTurnButton();
            startTitanTurnEvent.Invoke();
        });
        startTitanTurnEvent.AddListener(() =>
        {
            Debug.Log("Starting titan turn");
            endTitanTurnEvent.Invoke();
        });
        endTitanTurnEvent.AddListener(() =>
        {
            Debug.Log("Ending titan turn");
            currentTurn++;
            UpdateUI();
            startPlayerTurnEvent.Invoke();
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
    
    public void StartPlayerTurn()
    {
        startPlayerTurnEvent.Invoke();
    }
    public void EndPlayerTurn()
    {
        if (isPlayerTurn)
        {
            endPlayerTurnEvent.Invoke();
        }
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

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
    
}
