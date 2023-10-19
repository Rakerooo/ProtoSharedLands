using System.Collections.Generic;
using UnityEngine;

namespace Proto2.PathFinding
{
    [RequireComponent(typeof(LineRenderer))]
    public class NewProtoPathRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float lineWidth = 1f;
        [SerializeField] private float lineAlpha0, lineAlpha25 = 0.5f, lineAlpha50 = 0.75f, lineAlpha100 = 1f;
        
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

            lineRenderer.colorGradient = new Gradient
            {
                colorKeys = new GradientColorKey[] { },
                alphaKeys = new GradientAlphaKey[]
                {
                    new () { alpha = lineAlpha0, time = 0 },
                    new () { alpha = lineAlpha25, time = 0.25f },
                    new () { alpha = lineAlpha50, time = 0.5f },
                    new () { alpha = lineAlpha100, time = 1f }
                },
                mode = GradientMode.Blend,
                colorSpace = ColorSpace.Gamma
            };
        }
    }
}
