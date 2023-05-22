using Map.Grid;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(HexRenderer))]
    public class Hexagon : MonoBehaviour, IHoverable, ISelectable
    {
        private Vector2Int positionInGrid;
        private Region region;
        private HexagonType type;
        private HexRenderer hexRenderer;

        public void Init(Vector2Int _posInGrid, Material _material, float _innerSize, float _outerSize, float _height, bool _isFlatTopped)
        {
            hexRenderer = GetComponent<HexRenderer>();
            gameObject.layer += LayerMask.NameToLayer("Hoverable");
            positionInGrid = _posInGrid;
            hexRenderer.InstantiateRenderer(_material, _innerSize, _outerSize, _height, _isFlatTopped);
        }

        public void OnHoverEnable()
        {
            Debug.Log("hover");
        }

        public void OnHoverDisable()
        {
            Debug.Log("unhover");
        }

        public void OnSelectItem()
        {
            Debug.Log("selected");
        }

        public void OnDeselectItem()
        {
            Debug.Log("deselected");
        }
    }
}
