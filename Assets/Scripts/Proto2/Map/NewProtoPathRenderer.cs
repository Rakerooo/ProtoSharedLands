using System;
using System.Collections.Generic;
using UnityEngine;

namespace Proto2.Map
{
    [RequireComponent(typeof(LineRenderer))]
    public class NewProtoPathRenderer : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetLine(List<Vector3> positions)
        {
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }
    }
}
