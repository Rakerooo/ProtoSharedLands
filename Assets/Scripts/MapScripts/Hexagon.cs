using MapScripts.Grid;
using UnityEngine;

namespace MapScripts
{
    [RequireComponent(typeof(HexRenderer))]
    public class Hexagon : MonoBehaviour, IHoverable, ISelectable
    {
        public Vector2Int positionInGrid { get; private set; }
        private Region region;
        private HexagonType type;
        private HexRenderer hexRenderer;
        private Map map;

        private bool selected, hovered;

        private void Awake()
        {
            hexRenderer = GetComponent<HexRenderer>();
        }
        
        private void Start()
        {
            if (GameManager.instance) map = GameManager.instance.Map;
        }

        public void Init(Vector2Int _posInGrid, Material _mat, float _innerSize, float _outerSize, float _height, bool _isFlatTopped)
        {
            Awake();
            gameObject.layer += LayerMask.NameToLayer("Hoverable");
            positionInGrid = _posInGrid;
            hexRenderer.Init(_mat, _innerSize, _outerSize, _height, _isFlatTopped);
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
            map.SetSelectedHexagon(this);
        }

        public void OnDeselectItem()
        {
            selected = false;
            UpdateMat();
        }

        private void UpdateMat()
        {
            Debug.Log(hexRenderer);
            Debug.Log(GameManager.instance);
            if (selected) hexRenderer.SetColor(GameManager.instance.HexColors.selected);
            else if (hovered) hexRenderer.SetColor(GameManager.instance.HexColors.hovered);
            else hexRenderer.SetColor(GameManager.instance.HexColors.basic);
        }
    }
}
