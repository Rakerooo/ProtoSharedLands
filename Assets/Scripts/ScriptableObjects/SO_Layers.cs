using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/Layers", fileName = "Layers")]
    public class SO_Layers : ScriptableObject
    {
        [SerializeField] public LayerMask hoverableMask;
    }
}
