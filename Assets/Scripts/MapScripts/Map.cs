using UnityEngine;

namespace MapScripts
{
    public class Map : MonoBehaviour
    {
        public Hexagon SelectedHexagon { get; private set; }

        public void SetSelectedHexagon(Hexagon hexagon)
        {
            if (IsSelectedHexagon(hexagon))
            {
                hexagon.OnDeselectItem();
                SelectedHexagon = null;
            }
            else {SelectedHexagon = hexagon;}
        }

        public bool IsSelectedHexagon(Hexagon hexagon)
        {
            return Equals(SelectedHexagon, hexagon);
        }
    }
}
