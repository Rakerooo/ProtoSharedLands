using System.Collections.Generic;
using Proto2.PathFinding;
using UnityEngine;

namespace Proto2.Map
{
    public class NewProtoRegion : NewProtoPathPoint<NewProtoRegion>
    {
        [SerializeField] private List<NewProtoCell> cells;
        [SerializeField] private RegionResourceHandler resourceHandler;

        private List<NewProtoCell> Cells => cells;

        private void Start()
        {
            foreach (var cell in Cells)
            {
                cell.SetRegion(this);
            }
        }

        public void RefillRegion(float amount)
        {
            resourceHandler.RefillRegionResource(amount);
        }
        
        public void UpdateResourceHandlerUI()
        {
            resourceHandler.UpdateUI();
        }

        public RegionResourceHandler GetResourceHandler()
        {
            return resourceHandler;
        }
    }
}
