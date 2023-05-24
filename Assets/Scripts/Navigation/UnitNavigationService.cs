using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNavigationService : MonoBehaviour
{
    private INavUnit currentSelected;

    public void SelectUnit(INavUnit unit)
    {
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
}
