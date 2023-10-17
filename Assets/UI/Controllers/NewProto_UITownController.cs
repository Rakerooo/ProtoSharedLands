using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public enum ResourcesTypes
{
    Population,
    Ore,
    Wheat,
    Clay
}

public class NewProto_UITownController : MonoBehaviour
{

    [SerializeField] CanvasGroup group;

    //Header
    [Header("Town Header")]
    [SerializeField] TMP_Text nameField;
    [SerializeField] TMP_Text populationGain;

    //Ressources gain
    [Header("Ressources Gain")]
    [SerializeField] TMP_Text standardOreGain;
    [SerializeField] TMP_Text exploitOreGain;
    [SerializeField] TMP_Text standardWheatGain;
    [SerializeField] TMP_Text exploitWheatGain;
    [SerializeField] TMP_Text standardClayGain;
    [SerializeField] TMP_Text exploitClayGain;

    //Bâtiments
    [Header("Batiment Section")]
    [SerializeField] RectTransform plusSignButton;
    [SerializeField] RectTransform batimentButton;

    [Space(10f)]
    public UnityEvent onPlusbatiment;

    public void SetTownName(string TownName)
    {
        nameField.text = TownName;
    }

    public void SetPopulationGain(int gainByTurn)
    {
        populationGain.text = $"+{populationGain}";
    }

    public void SetNormalExploitationGain(int gainByTurn, ResourcesTypes resource)
    {
        switch(resource)
        {
            case ResourcesTypes.Ore:
            {
                standardOreGain.text = $"+{gainByTurn}";
                break;
            }
            case ResourcesTypes.Clay:
            {
                standardClayGain.text = $"+{gainByTurn}";
                break;
            }
            case ResourcesTypes.Wheat:
            {
                standardWheatGain.text = $"+{gainByTurn}";
                break;
            }
        }
    }

    public void SetHardExploitationGain(int gainByTurn, ResourcesTypes resource)
    {
        switch (resource)
        {
            case ResourcesTypes.Ore:
                {
                    exploitOreGain.text = $"+{gainByTurn}";
                    break;
                }
            case ResourcesTypes.Clay:
                {
                    exploitClayGain.text = $"+{gainByTurn}";
                    break;
                }
            case ResourcesTypes.Wheat:
                {
                    exploitWheatGain.text = $"+{gainByTurn}";
                    break;
                }
        }
    }

    public void CreateBatiment()
    {
        batimentButton.gameObject.SetActive(true);
        plusSignButton.gameObject.SetActive(false);

        onPlusbatiment?.Invoke();
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
        float delta = 0.05f;

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
        float delta = 0.05f;

        group.alpha = 0f;

        while(counter >= 0f)
        {
            counter -= delta;
            group.alpha = counter;
            yield return wait;
        }
    }
}
