using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnsUIController : MonoBehaviour
{

    public GameObject[] ChoiceBtnObjArr;

    public GameObject BtnNextObj;

    public void UpdChoiseBtnTexts(string s)
    {
        string[] parts = new string[ChoiceBtnObjArr.Length];

        bool mightSpeak = s != null;
        if (mightSpeak)
        {
            parts = s.Split(':');
        }

        for (int i = 0; i < ChoiceBtnObjArr.Length; i++)
        {
            ChoiceBtnObjArr[i].GetComponentInChildren<Text>().text = parts[i];
            ChoiceBtnObjArr[i].SetActive(mightSpeak);
        }

        BtnNextObj.SetActive(!mightSpeak);
    }
}
