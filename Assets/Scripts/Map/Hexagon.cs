using System;
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

        private bool selected, hovered;

        private void Awake()
        {
            hexRenderer = GetComponent<HexRenderer>();
        }

        public void Init(Vector2Int _posInGrid, float _innerSize, float _outerSize, float _height, bool _isFlatTopped)
        {
            Awake();
            gameObject.layer += GameManager.instance.Layers.hoverableMask;
            positionInGrid = _posInGrid;
            hexRenderer.InstantiateRenderer(_innerSize, _outerSize, _height, _isFlatTopped);
        }

        public void OnHoverEnable()
        {
            hovered = true;
            UpdateMat();
        }

        public void OnHoverDisable()
        {
            hovered = false;
            UpdateMat();
        }

        public void OnSelectItem()
        {
            selected = true;
            UpdateMat();
        }

        public void OnDeselectItem()
        {
            selected = false;
            UpdateMat();
        }

        private void UpdateMat()
        {
            if (selected) hexRenderer.SetMaterial(GameManager.instance.HexMats.selected);
            else if (hovered) hexRenderer.SetMaterial(GameManager.instance.HexMats.hovered);
            else hexRenderer.SetMaterial(GameManager.instance.HexMats.basic);
        }
    }
}
