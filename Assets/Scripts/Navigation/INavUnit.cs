using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INavUnit
{

    public void MoveToDestination(Vector3 destination);
    public void MoveToDestination(Hexagon hexagonDestination);
}
