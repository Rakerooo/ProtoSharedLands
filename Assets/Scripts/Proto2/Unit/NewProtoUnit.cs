using Proto2.PathFinding;
using UnityEngine;

namespace Proto2.Unit
{
    public abstract class NewProtoUnit<T> : MonoBehaviour where T : NewProtoPathPoint<T>
    {
        [SerializeField] private T startPos;
        private T currentPos;

        private void Start()
        {
            currentPos = startPos;
        }
    }
}
