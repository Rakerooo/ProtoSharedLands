using System.Collections.Generic;
using System.Linq;
using Proto2.Map;
using Proto2.PathFinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Proto2.Unit
{
    public class NewProtoTitan : NewProtoUnit<NewProtoRegion>
    {
        private readonly List<NewProtoRegion> possibleTargets = new();
        [SerializeField] protected float reloadRegionAmount = 10f;

        private new void Start()
        {
            base.Start();
        }
        
        public void StartTurn()
        {
            UpdateTargetPos();
            StartCoroutine(MoveToTargetPos());
        }

        private void UpdateTargetPos()
        {
            if (currentPos == null) return;
            currentPos.RefillRegion(reloadRegionAmount);
            
            if (finalTargetPos == null || finalTargetPos.Equals(currentPos) || pathIndex >= path.Count)
            {
                possibleTargets.Clear();
                possibleTargets.AddRange(possiblePositions);
                possibleTargets.Remove(currentPos);
                finalTargetPos = possibleTargets[Random.Range(0, possibleTargets.Count)];
                path.Clear();
                path.AddRange(NewProtoPathFinding<NewProtoRegion>.GetFullPath(currentPos, finalTargetPos, possiblePositions, true));
                pathIndex = 0;
                pathIndex++;
            }
            pathRenderer.SetLine(path.Select(c => c.Node.position).ToList());
            targetPos = path[pathIndex];
            pathIndex++;
        }
        
        protected override void MovementFinished()
        {
            pathRenderer.SetLine(new List<Vector3>());
            TurnManager.instance.EndTitanTurn();
        }
    }
}
