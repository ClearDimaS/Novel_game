using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ObjLocalisation : MonoBehaviour
{
    Text textField;

    public string key;

    void setTexts() 
    {
        textField = gameObject.GetComponent<Text>();

        string value = DialogueSystem.instance.localisationSystem.GetLocalisedValue(key);

        textField.text = value;
    }

    private void Start()
    {
        Invoke("setTexts", 0.1f);

        LocalisationSystem.onLangChange += setTexts;
    }


    void OnEnable()
    {
        LocalisationSystem.onLangChange += setTexts;
    }

    void OnDisable()
    {
        LocalisationSystem.onLangChange -= setTexts;
    }

    void OnDestroy()
    {
        LocalisationSystem.onLangChange -= setTexts;
    }

}
