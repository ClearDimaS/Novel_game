using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if the text can be fully suited in the Text Box. If not explicitly splits it into parts. Dialogue system class has simmilar checking, but there checking implies different Text parts which this class has no information about. 
/// Such double checking allows buliding a more complex logic from UserInput Class if necesseary.
/// </summary>
public class TextFormating
{
    DialogueSystem dialogue;
    public TextFormating() 
    {
        dialogue = DialogueSystem.instance;
    }

    string lastSpeaker = "";
    string[] speech_parts_tmp = new string[] { };

    public void Say(string s, ref int index)
    {
        string[] parts = s.Split(':');
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";

        bool overFlow = CheckOverflow(speech);

        if (overFlow)
            GetNextSpeechPart(ref speech, ref index);
        else
            ChangePhraseIndex(ref index);

        if (speaker == lastSpeaker)
            dialogue.SayAdd(speech, speaker);
        else
            dialogue.Say(speech, speaker);

        lastSpeaker = speaker;
    }

    private void GetNextSpeechPart(ref string speech, ref int index)
    {
        if (speech_parts_tmp.Length == 0)
            speech_parts_tmp = speech.Split(' ');

        speech = speech_parts_tmp[lastSubInd] + ' ';

        for (int i = lastSubInd + 1; !CheckOverflow(speech + speech_parts_tmp[i + 1]); i++)
        {
            speech += speech_parts_tmp[i] + ' ';

            lastSubInd = i;

            if (i == speech_parts_tmp.Length - 2)
            {
                ChangePhraseIndex(ref index);
                break;
            }
        }
    }

    private void resetSubInd()
    {
        lastSubInd = 0; 
        speech_parts_tmp = new string[] { };
    }

    int lastSubInd = 0;

    private bool CheckOverflow(string speech)
    {
        return (speech).ToCharArray().Length > dialogue.localisationSystem.GetMaxCharCnt();
    }

    private void ChangePhraseIndex(ref int index)
    {
        index++;
        resetSubInd();
    }
}
