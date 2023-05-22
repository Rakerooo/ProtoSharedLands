using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Layers", fileName = "Layers")]
public class SO_Layers : ScriptableObject
{
    [SerializeField] public LayerMask hoverableMask;
}
