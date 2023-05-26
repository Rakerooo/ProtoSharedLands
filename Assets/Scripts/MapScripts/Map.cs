using System;
using UnityEngine;

namespace MapScripts
{
    public class Map : MonoBehaviour
    {
        public Hexagon SelectedHexagon { get; private set; }

        public void SetSelectedHexagon(Hexagon hexagon)
        {
            if (SelectedHexagon)
            {
                SelectedHexagon.OnDeselectItem();
                SelectedHexagon = IsSelectedHexagon(hexagon) ? null : hexagon;
            }
            else SelectedHexagon = hexagon;
        }

        public bool IsSelectedHexagon(Hexagon hexagon)
        {
            return Equals(SelectedHexagon, hexagon);
        }
    }
}
