using Map.Grid;
using Unity.VisualScripting;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(HexRenderer))]
    public class Hexagon : MonoBehaviour
    {
        private Vector2Int positionInGrid;
        private Region region;
        private HexagonType type;
        private HexRenderer hexRenderer;

        public void Init(Vector2Int _posInGrid, Material _material, float _innerSize, float _outerSize, float _height, bool _isFlatTopped)
        {
            hexRenderer = GetComponent<HexRenderer>();
            
            positionInGrid = _posInGrid;
            hexRenderer.InstantiateRenderer(_material, _innerSize, _outerSize, _height, _isFlatTopped);
        }
    }
}
