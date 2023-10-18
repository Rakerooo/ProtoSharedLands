using System.Collections.Generic;
using UnityEngine;

namespace Proto2.PathFinding
{
    public class NewProtoPathPoint<T> : MonoBehaviour where T : NewProtoPathPoint<T>
    {
        [SerializeField] private List<T> neighbours;
        [SerializeField] private Transform node;
        [SerializeField] private float movementFactor = 1f;

        public List<T> Neighbours => neighbours;
        public Transform Node => node;
        public float MovementFactor => movementFactor;
        public float Distance { get; private set; }
        public bool Added { get; private set; }
        public T Parent { get; private set; }
        
        public void SetDistance(float newDistance)
        {
            Distance = newDistance;
        }
        public void SetAdded(bool added)
        {
            Added = added;
        }
        public void SetParent(T parent)
        {
            Parent = parent;
        }
    }
}
