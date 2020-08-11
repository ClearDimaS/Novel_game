using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalisationSystem
{

    public enum Language
    {
        English,
        Russian,
    }

    public static Language language = Language.English;

    private Dictionary<string, string> localisedEN;
    private Dictionary<string, string> localisedRU;

    public void Init(string _sceneName)
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV(_sceneName);

        localisedEN = csvLoader.GetDictionaryValues(Constants.EN);
        localisedRU = csvLoader.GetDictionaryValues(Constants.RU);
    }

    public static event OnLangChange onLangChange;

    public delegate void OnLangChange();

    public static void SetLanguge(Language lang) 
    {
        language = lang;

        onLangChange?.Invoke();
    }

    /// <summary>
    /// Gets localised string by its key. If the scene was changed the first call of this method should NOT be used before Init method.
    /// </summary>
    public string GetLocalisedValue(string key) 
    {
        string value = key;

        switch (language) 
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Russian:
                localisedRU.TryGetValue(key, out value);
                break;
        }

        return value;
    }

    public int GetDictLength() 
    {
        switch (language)
        {
            case Language.English:
                return localisedEN.Count - 4;
            case Language.Russian:
                return localisedRU.Count - 4;
            default:
                return localisedEN.Count - 4;
        }
    }

    public int GetMaxCharCnt()
    {
        switch (language)
        {
            case Language.English:
                return Constants.MAX_CHAR_CNT_EN;
            case Language.Russian:
                return Constants.MAX_CHAR_CNT_RU;
            default:
                return Constants.MAX_CHAR_CNT_EN;
        }
    }
}
