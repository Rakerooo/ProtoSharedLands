using ScriptableObjects;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private SO_Layers soLayers;
    [SerializeField] private SO_HexMats soHexMats;

    public SO_Layers Layers => soLayers;
    public SO_HexMats HexMats => soHexMats;
    
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
    }
}
