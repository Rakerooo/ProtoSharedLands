using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/HexColors", fileName = "HexColors")]
    public class SO_HexColors : ScriptableObject
    {
        [SerializeField] public Color basicIn;
        [SerializeField] public Color basicOut;
        [SerializeField] public Color hovered;
        [SerializeField] public Color selected;
    }
}
