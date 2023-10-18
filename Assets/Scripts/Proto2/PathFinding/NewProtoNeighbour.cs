using System;
using UnityEngine;

namespace Proto2.Map
{
    [Serializable]
    public struct NewProtoNeighbour
    {
        [SerializeField] private NewProtoCell cell;

        public NewProtoCell Cell => cell;
        public float Distance { get; private set; }

        public void SetDistance(float newDistance)
        {
            Distance = newDistance;
        }
    }
}