using System.Collections.Generic;
using UnityEngine;

namespace Proto2.PathFinding
{
    [RequireComponent(typeof(LineRenderer))]
    public class NewProtoPathRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float lineWidth = 1f;
        
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void OnValidate()
        {
            if (lineRenderer == null) Awake();
            lineRenderer.endWidth = lineWidth;
            lineRenderer.startWidth = lineWidth;
        }

        public void SetLine(List<Vector3> positions)
        {
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }
    }
}
