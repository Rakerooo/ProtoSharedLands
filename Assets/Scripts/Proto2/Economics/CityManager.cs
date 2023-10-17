using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    [SerializeField] private CityResourceGatherer _resourceGatherer;
    [SerializeField] private CityBuildingHandler _buildingHandler;

    private void Awake()
    {
        if (_resourceGatherer != null)
        {
            _resourceGatherer.SetCityManager(this);
        }

        if (_buildingHandler != null)
        {
            _buildingHandler.SetCityManager(this);
        }
        
    }
    
    public CityResourceGatherer GetResourceGatherer()
    {
        return _resourceGatherer;
    }

    public CityBuildingHandler GetBuildingHandler()
    {
        return _buildingHandler;
    }
}
