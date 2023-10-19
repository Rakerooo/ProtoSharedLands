using System;
using System.Collections;
using System.Collections.Generic;
using Proto2.Map;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private NewProto_UITownController _cityUIController;
    [SerializeField] private NewProto_UIRegionController _regionUIController;
    [SerializeField] private bool isCityUiEnabled;
    [SerializeField] private bool isRegionUiEnabled;

    private NewProtoCell currentSelectCell;

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
        if (!isCityUiEnabled) return;
        _cityUIController.ToggleWindow(false);
        isCityUiEnabled = false;
    }
    public void EnableCityUI()
    {
        if (isCityUiEnabled) return;
        _cityUIController.ToggleWindow(true);
        isCityUiEnabled = true;

    }
    public void DisableRegionUI()
    {
        if (!isRegionUiEnabled) return;
        _regionUIController.ToggleWindow(false);
        isRegionUiEnabled = false;
        DisableCityUI();
    }
    public void EnableRegionUI()
    {
        if (isRegionUiEnabled) return;
        _regionUIController.ToggleWindow(true);
        isRegionUiEnabled = true;
    }

    public bool IsCityUiEnabled()
    {
        return isCityUiEnabled;
    }
    public bool IsRegionUiEnabled()
    {
        return isRegionUiEnabled;
    }

    public void SetCurrentSelectedCell(NewProtoCell selectedCell)
    {
        currentSelectCell = selectedCell;
    }

    public void DeselectCell()
    {
        currentSelectCell = null;
    }
}
