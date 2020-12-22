using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMiscs : MonoBehaviour
{
    [HideInInspector]
    public InformationList[] informations = new InformationList[3];

    private void Start()
    {
        for(int i = 0; i < informations.Length; i++)
        {
            informations[i].information = LanguageSystem.lang.information[i];
        }
    }
}

[System.Serializable]
public class InformationList
{
    public string information;
}