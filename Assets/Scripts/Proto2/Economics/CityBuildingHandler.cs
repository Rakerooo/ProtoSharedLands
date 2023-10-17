using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Serialization;

public class CityBuildingHandler : MonoBehaviour
{ 
    private CityManager _cityManager;
    [SerializeField] private List<Building> buildableBuildings;
    [SerializeField] private List<Building> currentBuildings;

    private void Awake()
    {
        foreach (var building in currentBuildings)
        {
            building.SetCityBuildingHandler(this);
        }
    }

    public void SetCityManager(CityManager pCityManager)
    {
        _cityManager = pCityManager;
    }
    
    public IEnumerable<Building> GetCurrentBuildings()
    {
        return currentBuildings;
    }

    public void Build(Building building)
    {
        if (building.GetCost() <  PlayerResourceManager.instance.GetCurrentResource())
        {
            Debug.Log("Mettre le code de construction de bâtiment ici");
        }
        else
        {
            Debug.Log("Pas assez de ressource");
        }
    }
    
}
