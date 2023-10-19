using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    [SerializeField] private NewProto_UITownController _cityUIController;
    [SerializeField] private NewProto_UIRegionController _regionUIController;
    [SerializeField] private bool isCityUiEnabled;
    [SerializeField] private bool isRegionUiEnabled;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    public void DisableCityUI()
    {
        _cityUIController.ToggleWindow(false);
        isCityUiEnabled = false;
    }
    public void EnableCityUI()
    {
        _cityUIController.ToggleWindow(true);
        isCityUiEnabled = true;
    }

    public bool IsCityUiEnabled()
    {
        return isCityUiEnabled;
    }
    public bool IsRegionUiEnabled()
    {
        return isRegionUiEnabled;
    }
}
