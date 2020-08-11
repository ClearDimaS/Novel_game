using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        localisationSystem = new LocalisationSystem();

        localisationSystemPlayer = new LocalisationSystem();
    }

    [HideInInspector]
    public LocalisationSystem localisationSystem;

    [HideInInspector]
    public LocalisationSystem localisationSystemPlayer;

    public ELEMENTS elements;

    /// <summary>
    /// Say something and display it in the speech box
    /// </summary>
    public void Say(string speech, string speaker = "") 
    {
        StopSpeaking();

        speaking = StartCoroutine(Speaking(speech, false, speaker));
    }

    /// <summary>
    /// Say something to be added to what is already on the speech box
    /// </summary>
    public void SayAdd(string speech, string speaker = "") 
    {
        StopSpeaking();

        speechText.text = targetSpeech;

        if (CheckOverflowAdd(speech)) 
        {
            Say(speech, speaker);
            return;
        }
        speaking = StartCoroutine(Speaking(speech, true, speaker));
    }

    private bool CheckOverflowAdd(string speech)
    {
        return (targetSpeech + speech).ToCharArray().Length > localisationSystem.GetMaxCharCnt();
    }

    public void StopSpeaking() 
    {
        if (isSpeaking) 
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }

    public bool isSpeaking { get { return speaking != null; } }
    [HideInInspector] public bool isWaitingForUserInput = false;

    string targetSpeech;
    Coroutine speaking = null;
    IEnumerator Speaking(string speech, bool additive, string speaker = "")
    {
        speechPanel.SetActive(true);
        targetSpeech = speech;

        if (!additive)
            speechText.text = "";
        else
            targetSpeech = speechText.text + targetSpeech;

        speakerNameText.text = DetermineSpeaker(speaker);
        isWaitingForUserInput = false;

        while (speechText.text != targetSpeech) 
        {
            speechText.text += targetSpeech[speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        // Displaying speech text finished
        isWaitingForUserInput = true;
        while (isWaitingForUserInput) 
        {
            yield return new WaitForEndOfFrame();
        }

        StopSpeaking();
    }

    private string DetermineSpeaker(string s)
    {
        string retVal = speakerNameText.text; // default return the current name
        if (s != speakerNameText.text && s != "") 
            retVal = (s.ToLower().Contains(Constants.NARRATOR)) ? "" : s;

        return retVal;
    }

    [System.Serializable]
    public class ELEMENTS 
    {
        /// <summary>
        /// A class containing all UI dialogue elements
        /// </summary>
        /// 
        public GameObject speechPanel;
        public Text speakerNameText;
        public Text speechText;
        public Image speaker;
    }
    public GameObject speechPanel { get { return elements.speechPanel; } }
    public Text speakerNameText { get { return elements.speakerNameText; } }
    public Text speechText { get { return elements.speechText; } }
}
