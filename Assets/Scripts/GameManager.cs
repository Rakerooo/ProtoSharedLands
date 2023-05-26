using System.Collections;
using MapScripts;
using ScriptableObjects;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private SO_Layers soLayers;
    [SerializeField] private SO_HexColors soHexColors;
    public SO_Layers Layers => soLayers;
    public SO_HexColors HexColors => soHexColors;

    [SerializeField] private Map map;

    [SerializeField] private bool canPlay;
    [SerializeField] private float turnTimeCoolDown;
    public Map Map => map;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        canPlay = true;
    }
    public void SetCanPlay(bool value)
    {
        Debug.Log($"Can play ? {value}");
        canPlay = value;
    }public bool GetCanPlay()
    {
        return canPlay;
    }
    
}
