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

    private void Start()
    {
        _cityManager.GetTownUIController().onPlusbatiment.AddListener(()=>Build(buildableBuildings[0]));
    }

    public void SetCityManager(CityManager pCityManager)
    {
        _cityManager = pCityManager;
    }
    
    public IEnumerable<Building> GetCurrentBuildings()
    {
        return currentBuildings;
    }

    public bool Build(Building building)
    {
        if (building.GetCost() <=  PlayerResourceManager.instance.GetCurrentResource())
        {
            if (buildableBuildings.Count > 0)
            {
                currentBuildings.Add(Instantiate(buildableBuildings[0]));
                PlayerResourceManager.instance.RemoveResource(building.GetCost());
                _cityManager.GetTownUIController().FillBuildingUI();
                _cityManager.UpdateUI();
            }
            else
            {
                Debug.Log("Pas de building dans la liste");
            }
            return true;
        }
        else
        {
            Debug.Log("Pas assez de ressource");
            return false;
        }
    }
    
}
