using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public abstract class Event
{
    protected UIDocument eventUI;
    protected byte minTurn;
    protected byte maxTurn;
    protected UnityEvent[] completionEvents;
}
