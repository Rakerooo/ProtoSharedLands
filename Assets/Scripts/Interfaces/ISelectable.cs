using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    public void OnSelectItem();
    public void OnDeselectItem();
    public void OnAlternateSelect();
    public void OnAlternateDeselect();
    
}
