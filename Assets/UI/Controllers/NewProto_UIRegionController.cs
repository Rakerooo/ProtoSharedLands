using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class NewProto_UIRegionController : MonoBehaviour
{
    [Header("Region Name")]
    [SerializeField] TMP_Text regionName;

    //Ressources
    [Header("Region Resources")]
    [SerializeField] TMP_Text oreGain;
    [SerializeField] TMP_Text wheatGain;
    [SerializeField] TMP_Text clayGain;

    //Exploitation Meter
    [Header("Exploitation")]
    [SerializeField] Image exploitationBar;

    public UnityEvent<bool> onHardExploitToggle;

    public void SetRegionName(string regionName)
    {
        this.regionName.text = regionName;
    }

    public void HardExploitToggle(bool value)
    {
        onHardExploitToggle?.Invoke(value);
    }

    public void SetExploitationGain(ResourcesTypes resource, int gainByTurn)
    {
        switch (resource)
        {
            case ResourcesTypes.Ore:
                {
                    oreGain.text = $"+{gainByTurn}";
                    break;
                }
            case ResourcesTypes.Clay:
                {
                    clayGain.text = $"+{gainByTurn}";
                    break;
                }
            case ResourcesTypes.Wheat:
                {
                    wheatGain.text = $"+{gainByTurn}";
                    break;
                }
        }
    }

    public void UpdateExplotationRate(float rate)
    {
        exploitationBar.fillAmount = rate;
    }
}
