using System;
using System.Collections;
using System.Collections.Generic;
using Proto2.Map;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private NewProto_UITownController _cityUIController;
    [SerializeField] private NewProto_UIRegionController _regionUIController;
    [SerializeField] private Button endTurnButton;
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
        _cityUIController.gameObject.SetActive(false);
    }
    public void EnableCityUI()
    {
        if (isCityUiEnabled) return;
        _cityUIController.gameObject.SetActive(true);
        _cityUIController.ToggleWindow(true);
        isCityUiEnabled = true;

    }
    public void DisableRegionUI()
    {
        if (!isRegionUiEnabled) return;
        _regionUIController.ToggleWindow(false);
        isRegionUiEnabled = false;
        DisableCityUI();
        _regionUIController.gameObject.SetActive(false);
    }
    public void EnableRegionUI()
    {
        if (isRegionUiEnabled) return;
        _regionUIController.gameObject.SetActive(true);
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

    public void EnableEndTurnButton()
    {
        endTurnButton.gameObject.SetActive(true);
    }
    public void DisableEndTurnButton()
    {
        endTurnButton.gameObject.SetActive(false);
    }
}
