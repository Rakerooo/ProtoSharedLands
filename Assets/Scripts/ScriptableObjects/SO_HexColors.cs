using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/HexColors", fileName = "HexColors")]
    public class SO_HexColors : ScriptableObject
    {
        [SerializeField] public Color hovered;
        [SerializeField] public Color selected;
        [SerializeField] public Color basic;
    }
}
