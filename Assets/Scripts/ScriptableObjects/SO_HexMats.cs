using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/HexMats", fileName = "HexMats")]
    public class SO_HexMats : ScriptableObject
    {
        [SerializeField] public Material hovered;
        [SerializeField] public Material selected;
        [SerializeField] public Material basic;
    }
}
