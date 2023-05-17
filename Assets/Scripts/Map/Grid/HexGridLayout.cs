using UnityEngine;

namespace Map.Grid
{
    public class HexGridLayout : MonoBehaviour
    {
        [Header("Grid settings")]
        [SerializeField] private Vector2Int gridSize;
    
        [Header("Tile settings")]
        [SerializeField] private Material material;
        [SerializeField] private float innerSize;
        [SerializeField] private float outerSize = 10f;
        [SerializeField] private float height = 1f;
        [SerializeField] private bool isFlatTopped;

        private readonly float sqrt3 = Mathf.Sqrt(3);

        [ContextMenu("Refresh")]
        private void UpdateGrid()
        {
            LayoutGrid(true);
        }
        
        private void OnEnable()
        {
            LayoutGrid(false);
        }

        private void LayoutGrid(bool editor)
        {
            var goTransform = gameObject.transform;
            var childCount = goTransform.childCount;
            for (var i = childCount - 1; i >= 0; i--)
            {
                if (editor) DestroyImmediate(goTransform.GetChild(i).gameObject);
                else Destroy(goTransform.GetChild(i).gameObject);
            }
            
            for (var y = 0; y < gridSize.y; y++)
            {
                for (var x = 0; x < gridSize.x; x++)
                {
                    var pos = new Vector2Int(x, y);
                    var tile = new GameObject($"Hex ({x}, {y})", typeof(Hexagon));
                    var tileTransform = tile.transform;
                    tileTransform.position = GetHexPosFromCoordinates(pos);
                    tile.GetComponent<Hexagon>().Init(pos, material, innerSize, outerSize, height, isFlatTopped);
                    tileTransform.SetParent(transform, true);
                }
            }
        }

        private Vector3 GetHexPosFromCoordinates(Vector2Int _coords)
        {
            var x = _coords.x;
            var y = _coords.y;
            float width;
            float length;
            float xPos;
            float yPos;
            bool shouldOffset;
            float horizontalDist;
            float verticalDist;
            float offset;
            var size = outerSize;

            if (isFlatTopped)
            {
                shouldOffset = x % 2 == 0;
                width = 2f * size;
                length = sqrt3 * size;

                horizontalDist = width * 0.75f;
                verticalDist = length;

                offset = shouldOffset ? width / 2 : 0;

                xPos = x * horizontalDist;
                yPos = y * verticalDist - offset;
            }
            else
            {
                shouldOffset = y % 2 == 0;
                width = sqrt3 * size;
                length = 2f * size;

                horizontalDist = width;
                verticalDist = length * 0.75f;

                offset = shouldOffset ? width / 2 : 0;

                xPos = x * horizontalDist + offset;
                yPos = y * verticalDist;
            }

            return new Vector3(xPos, 0, -yPos);
        }
    }
}
