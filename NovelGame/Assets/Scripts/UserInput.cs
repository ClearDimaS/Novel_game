using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    public BtnsUIController btnsUIController;

    DialogueSystem dialogue;

    TextFormating formatText;

    string currentScene;

    int index;

    public WebInfo webInfo;

    BundleController bundleController;


    private void Start()
    {
        bundleController = new BundleController();

        dialogue = DialogueSystem.instance;

        formatText = new TextFormating();

        ChangeScene();
    }

    public void NextPhrase() 
    {
        if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
        {
            if (index > dialogue.localisationSystem.GetDictLength())
            {
                return;
            }

            btnsUIController.UpdChoiseBtnTexts(dialogue.localisationSystemPlayer.GetLocalisedValue(index.ToString()));

            formatText.Say(dialogue.localisationSystem.GetLocalisedValue(index.ToString()), ref index);
        }
    }


    public void ChangeLanguage()
    {
        if (LocalisationSystem.language == LocalisationSystem.Language.English)
            LocalisationSystem.SetLanguge(LocalisationSystem.Language.Russian);
        else
            LocalisationSystem.SetLanguge(LocalisationSystem.Language.English);
    }

    public void ChangeScene()
    {
        resetScene();
    }

    private void resetScene()
    {
        index = 1;

        updEmotions();

        dialogue.localisationSystem.Init(currentScene);

        dialogue.localisationSystemPlayer.Init(currentScene + "Player");

        NextPhrase();
    }

    private void updEmotions()
    {
        if (currentScene == Constants.SCENE2) { bundleController.loadBundle("", webInfo, dialogue.elements.speaker);  }

        if (currentScene == Constants.SCENE1)
            currentScene = Constants.SCENE2;
        else
            currentScene = Constants.SCENE1;

    }
}
