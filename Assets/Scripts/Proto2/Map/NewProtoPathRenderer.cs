using System;
using System.Collections.Generic;
using UnityEngine;

namespace Proto2.Map
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
            lineRenderer.endWidth = lineWidth;
            lineRenderer.startWidth = lineWidth;
        }

        public void SetLine(List<Vector3> positions)
        {
            Debug.Log(positions.Count);
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }
    }
}
