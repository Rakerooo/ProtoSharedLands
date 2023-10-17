using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionResourceHandler : MonoBehaviour
{
    [SerializeField] private CityResourceGatherer city;
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
}
