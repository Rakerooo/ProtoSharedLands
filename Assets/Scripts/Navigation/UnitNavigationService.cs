using MapScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNavigationService : MonoBehaviour
{
    private INavUnit currentSelected;

    
    public void SelectUnit(INavUnit unit)
    {
        if(currentSelected != null)
        {
            SimpleUnit previous = (SimpleUnit)unit;
            previous.OnDeselectItem();
        }

        currentSelected = unit;
    }

    public void DeselectAllUnit()
    {
        currentSelected = null; 
    }

    public void SetSelectedUnitOnMove(Vector3 targetDestination)
    {
        if (currentSelected != null)
        {
            currentSelected.MoveToDestination(targetDestination);
        }
    }

    public void HexSelected(Hexagon hex)
    {
        if(currentSelected != null)
        {
            currentSelected.MoveToDestination(hex.transform.position);

            currentSelected = null;
        }
    }
}
