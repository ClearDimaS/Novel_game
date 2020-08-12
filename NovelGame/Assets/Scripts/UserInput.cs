using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    public BtnsUIController btnsUIController;

    public ImageUpd imageUpd;

    DialogueSystem dialogue;

    TextFormating formatText;

    string currentScene;

    int index;


    private void Start()
    {
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
        updEmotions();

        resetScene();
    }

    private void resetScene()
    {
        resetDialogue();

        NextPhrase();
    }

    private void resetDialogue()
    {
        index = 1;

        dialogue.localisationSystem.Init(currentScene);

        dialogue.localisationSystemPlayer.Init(currentScene + "Player");
    }

    private void updEmotions()
    {
        if (currentScene == Constants.SCENE2) { imageUpd.LoadImage(); }

        if (currentScene == Constants.SCENE1)
            currentScene = Constants.SCENE2;
        else
            currentScene = Constants.SCENE1;

    }
}
