using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    [SerializeField] private CityResourceGatherer _resourceGatherer;
    [SerializeField] private CityBuildingHandler _buildingHandler;
    [SerializeField] private NewProto_UITownController _UIController;

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

    private void Start()
    {
        UpdateUI();
    }

    public CityResourceGatherer GetResourceGatherer()
    {
        return _resourceGatherer;
    }

    public CityBuildingHandler GetBuildingHandler()
    {
        return _buildingHandler;
    }

    public void UpdateUI()
    {
        _UIController.SetNormalExploitationGain((int)_resourceGatherer.GetProductionValue(), ResourcesTypes.Ore);
        _UIController.SetHardExploitationGain((int)_resourceGatherer.GetExploitationValue(), ResourcesTypes.Ore);
        _UIController.SetHardExploitToggle(_resourceGatherer.GetExploitationMode());
    }
}
