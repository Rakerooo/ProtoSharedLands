using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    [SerializeField]
    private byte turnCount;
    private Dictionary<byte, Event> events;
    
}
