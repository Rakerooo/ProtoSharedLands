using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public static PlayerResourceManager instance;

    [SerializeField] private NewProto_UITopBarController _UIcontroller;
    [SerializeField] private float currentResourceValue;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddResource(float resource)
    {
        currentResourceValue += resource;
        UpdateUI();
    }

    public void RemoveResource(float resource)
    {
        currentResourceValue -= resource;
        UpdateUI();
    }

    public float GetCurrentResource()
    {
        return currentResourceValue;
    }

    public void UpdateUI()
    {
        _UIcontroller.UpdateResourceStash(ResourcesTypes.Ore,(int)currentResourceValue);
    }
}
