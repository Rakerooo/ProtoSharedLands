using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class NewProto_UIRegionController : MonoBehaviour
{
    [SerializeField] CanvasGroup group;
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
    
    public void ToggleWindow(bool toggle)
    {
        StopAllCoroutines();

        if (toggle)
            StartCoroutine(ShowWindowRoutine());

        else
            StartCoroutine(HideWindowRoutine());
    }
    
    public IEnumerator ShowWindowRoutine()
    {
        var wait = new WaitForSeconds(.05f);

        float counter = 0f;
        float delta = 0.3f;

        group.alpha = 0f;

        while(counter <= 1f)
        {
            counter += delta;
            group.alpha = counter;
            yield return wait;
        }
    }

    public IEnumerator HideWindowRoutine()
    {
        var wait = new WaitForSeconds(.05f);

        float counter = 1f;
        float delta = 0.3f;

        group.alpha = 0f;

        while(counter >= 0f)
        {
            counter -= delta;
            group.alpha = counter;
            yield return wait;
        }
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

    public void UpdateExploitationRate(float rate)
    {
        exploitationBar.fillAmount = rate;
    }
}
