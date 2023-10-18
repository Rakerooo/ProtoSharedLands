using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewProto_UITopBarController : MonoBehaviour
{
    [Header("Resources Reserve")]
    [SerializeField] TMP_Text oreStash;
    [SerializeField] TMP_Text wheatStash;
    [SerializeField] TMP_Text clayStash;

    [Header("Turn Timer")]
    [SerializeField] TMP_Text turnField;

    [Header("Menu")]
    [SerializeField] RectTransform menuPanel;

    public void UpdateTurnCount(int turnTimer)
    {
        turnField.text = $"Turn {turnTimer}";
    }

    private bool menuVisibility = false;

    public void UpdateResourceStash(ResourcesTypes resource, int currentResourceAmount)
    {
        switch (resource)
        {
            case ResourcesTypes.Ore:
                {
                    oreStash.text = $"{currentResourceAmount}";
                    break;
                }
            case ResourcesTypes.Clay:
                {
                    clayStash.text = $"{currentResourceAmount}";
                    break;
                }
            case ResourcesTypes.Wheat:
                {
                    wheatStash.text = $"{currentResourceAmount}";
                    break;
                }
        }
    }

    public void ToggleMenu()
    {
        menuVisibility = !menuVisibility;

        menuPanel.gameObject.SetActive(menuVisibility);
    }

    public void MenuQuit()
    {
        Application.Quit();
    }
}
