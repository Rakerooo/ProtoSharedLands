using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RegionResourceHandler : MonoBehaviour
{
    [SerializeField] private CityResourceGatherer city;
    [SerializeField] private float prod;
    [SerializeField] private float currentResourceStock;
    [SerializeField] private float maxResourceStock;
    [SerializeField] private NewProto_UIRegionController _UIController;
    
    private void Awake()
    {
        if (city != null)
        {
            city.SetRegion(this);
        }
    }
    private void Start()
    {
        UpdateUI();
    }

    public float GetCurrentResourceStock()
    {
        return currentResourceStock;
    }

    public float GetProd()
    {
        return prod;
    }

    public void RemoveFromStock(float stockToRemove)
    {
        if (stockToRemove >= currentResourceStock)
        {
            currentResourceStock = 0;
        }
        else
        {
            currentResourceStock -= stockToRemove;
        }
    }

    public void UpdateUI()
    {
        _UIController.SetExploitationGain(ResourcesTypes.Ore,(int)prod);
        _UIController.UpdateExploitationRate(currentResourceStock/maxResourceStock);
    }
}
