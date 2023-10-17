using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityResourceGatherer : MonoBehaviour
{
    private RegionResourceHandler _regionResourceHandler;
    private CityManager _cityManager;
    [SerializeField] private bool exploitationMode = false;

    private void Start()
    {
        TurnManager.instance.GetStartPlayerTurnEvent().AddListener(() => GatherResources());
    }

    private void OnDestroy()
    {
        TurnManager.instance.GetStartPlayerTurnEvent().RemoveListener(() => GatherResources());
    }

    public void SetRegion(RegionResourceHandler pRegionResourceHandler)
    {
        if (_regionResourceHandler == null)
        {
            _regionResourceHandler = pRegionResourceHandler;
        }
    }

    public void SetCityManager(CityManager pCityManager)
    {
        _cityManager = pCityManager;
    }
    
    public float GetProductionValue(IEnumerable<Building> buildings)
    {
        return buildings.Sum(building => building.GetProductionValue());
    }

    public float GetExploitationValue(IEnumerable<Building> buildings)
    {
        return buildings.Sum(building => building.GetExploitationValue());
    }

    public void GatherResources()
    {
        IEnumerable<Building> buildings = _cityManager.GetBuildingHandler().GetCurrentBuildings();
        if (!exploitationMode)
        {
            var tmpResource = GetProductionValue(buildings) + _regionResourceHandler.GetProd();
            Debug.Log($"Region is not exploited. {tmpResource} gathered by city");
            PlayerResourceManager.instance.AddResource(tmpResource);
        }
        else
        {
            var tmpExploitationValue = GetExploitationValue(buildings);
            if (tmpExploitationValue > _regionResourceHandler.GetCurrentResourceStock())
            {
                tmpExploitationValue = _regionResourceHandler.GetCurrentResourceStock();
            }

            var resourceGathered = GetProductionValue(buildings) + _regionResourceHandler.GetProd() +
                                   tmpExploitationValue;
            Debug.Log($"Region is exploited. {resourceGathered} gathered by city. Region stock decreased by {tmpExploitationValue}. Region new stock : {_regionResourceHandler.GetCurrentResourceStock()-tmpExploitationValue}");
            PlayerResourceManager.instance.AddResource(resourceGathered);
            _regionResourceHandler.RemoveFromStock(tmpExploitationValue);
        }
    }

    public void ToggleExploitation()
    {
        exploitationMode = !exploitationMode;
    }
}