using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public static PlayerResourceManager instance;
    [SerializeField] private float currentResourceValue;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    public void AddResource(float resource)
    {
        currentResourceValue += resource;
    }

    public void RemoveResource(float resource)
    {
        currentResourceValue -= resource;
    }

    public float GetCurrentResource()
    {
        return currentResourceValue;
    }
}
