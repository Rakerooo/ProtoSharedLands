using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RegionResourceHandler : MonoBehaviour
{
    [SerializeField] private CityResourceGatherer city;
    [SerializeField] private NewProto_UIRegionController _UIController;
    [SerializeField] private float prod;
    [SerializeField] private float currentResourceStock;
    [SerializeField] private float maxResourceStock;
    
    
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
        if (city == null && UIManager.instance.IsCityUiEnabled())
        {
            UIManager.instance.DisableCityUI();
        }
        else if(city != null && !UIManager.instance.IsCityUiEnabled())
        {
            UIManager.instance.EnableCityUI();
        }
    }

    public void RefillRegionResource(float refillAmount)
    {
        if (currentResourceStock + refillAmount >= maxResourceStock)
        {
            currentResourceStock = maxResourceStock;
        }
        else
        {
            currentResourceStock += refillAmount;
        }
        UpdateUI();
    }
}
