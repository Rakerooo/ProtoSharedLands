using System.Collections.Generic;
using Proto2.PathFinding;
using UnityEngine;

namespace Proto2.Map
{
    public class NewProtoRegion : NewProtoPathPoint<NewProtoRegion>
    {
        [SerializeField] private List<NewProtoCell> cells;

        public List<NewProtoCell> Cells => cells;
    }
}
