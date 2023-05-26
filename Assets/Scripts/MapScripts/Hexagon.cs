using MapScripts.Grid;
using UnityEngine;

namespace MapScripts
{
    [RequireComponent(typeof(HexRenderer))]
    public class Hexagon : MonoBehaviour, IHoverable, ISelectable
    {
        [SerializeField] UnitNavigationService unitNavigationService;
        public Vector2Int positionInGrid { get; private set; }
        private Region region;
        private HexagonType type;
        private HexRenderer hexRendererOut, hexRendererIn;
        private Map map;

        private bool selected, hovered;

        private void Awake()
        {
            hexRendererIn = GetComponent<HexRenderer>();
            if (transform.childCount == 0)
            {
                var border = new GameObject("Border").gameObject;
                var hexagonTransform = transform;
                border.transform.position = hexagonTransform.position;
                border.transform.SetParent(hexagonTransform);
                hexRendererOut = border.AddComponent<HexRenderer>();
            }
            else
            {
                hexRendererOut = transform.GetChild(0).gameObject.GetComponent<HexRenderer>();
            }
        }
        
        private void Start()
        {
            if (GameManager.instance) map = GameManager.instance.Map;
        }

        public void Init(Vector2Int _posInGrid, Material _matIn, Material _matOut, float _innerSize, float _outerSize, float _height, bool _isFlatTopped)
        {
            Awake();
            gameObject.layer += LayerMask.NameToLayer("Hoverable");
            positionInGrid = _posInGrid;
            hexRendererIn.Init(_matIn, 0, _innerSize, _height, _isFlatTopped);
            hexRendererOut.Init(_matOut, _innerSize, _outerSize, _height, _isFlatTopped);
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

        public void OnAlternateSelect()
        {
            //throw new System.NotImplementedException();
            if(unitNavigationService != null)
            {
                unitNavigationService.HexSelected(this);
            }
        }

        public void OnAlternateDeselect()
        {
            //Pas besoin

            //throw new System.NotImplementedException();
        }

        private void UpdateMat()
        {
            if (selected) hexRendererIn.SetColor(GameManager.instance.HexColors.selected);
            else if (hovered) hexRendererIn.SetColor(GameManager.instance.HexColors.hovered);
            else hexRendererIn.SetColor(GameManager.instance.HexColors.basicIn);
        }
    }
}
