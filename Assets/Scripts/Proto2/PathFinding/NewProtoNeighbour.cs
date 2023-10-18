using System;
using Proto2.Map;
using UnityEngine;

namespace Proto2.PathFinding
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