using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MapScripts;

public class UI_SelectInfo : MonoBehaviour
{
    [SerializeField] RectTransform root;

    [Header("Properties")]
    [SerializeField] TMP_Text header;
    [SerializeField] TMP_Text body;


    public void TogglePannel(bool toggle)
    {
        root.gameObject.SetActive(toggle);
    }

    public void DisplaySelectionInfo(ISelectable instance)
    {
        if (instance == null) return;

        switch(instance)
        {
            case SimpleUnit:
            {
                header.text = "Simple Unit";
                body.text = "Right click on an Hexagon to move there";
                break;
            }
            case Hexagon:
            {
                header.text = "Hexagon";
                body.text = "Just an Hexagon";
                break;
            }

        }
    }
}
