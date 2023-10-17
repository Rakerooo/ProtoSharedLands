using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private CityBuildingHandler _cityBuildingHandler;
    [SerializeField] private ushort cost = 10;
    [SerializeField] private float productionValue = 2;
    [SerializeField] private float exploitationValue = 4;
    
    public ushort GetCost()
    {
        return cost;
    }

    public float GetProductionValue()
    {
        return productionValue;
    }

    public float GetExploitationValue()
    {
        return exploitationValue;
    }

    public void SetCityBuildingHandler(CityBuildingHandler pCity)
    {
        if (_cityBuildingHandler == null)
        {
            _cityBuildingHandler = pCity;
        }
    }
}
