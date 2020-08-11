﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using UnityEditor;

public class CSVLoader
{
    //Reference file;
    private TextAsset csvFile;
    private char lineSeperator = '\n';
    private char surround = '"';
    private string[] fieldSeperator = { "\", \"" };


    public void LoadCSV(string sceneName)
    {
        csvFile = Resources.Load<TextAsset>("localisation" + sceneName);
    }

    public Dictionary<string, string> GetDictionaryValues(string attributeId) 
    {
        Dictionary<string, string> dictrionary = new Dictionary<string, string>();

        string[] lines = csvFile.text.Split(lineSeperator);
        
        int attributeIndex = -1;

        string[] headers = lines[0].Split(fieldSeperator, System.StringSplitOptions.None);


        for (int i=0; i<headers.Length; i++) 
        {
            if (headers[i].Contains(attributeId)) 
            {
                attributeIndex = i;
                break;
            }
        }

        if (attributeIndex == -1) 
        {
            Debug.LogWarning("Wrong language key");
            return dictrionary;
        }


        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for (int i=1; i<lines.Length; i++) 
        {
            string line = lines[i];

            string[] fields = CSVParser.Split(line);

            for (int f = 0; f < fields.Length; f++) 
            {
                fields[f] = fields[f].TrimStart(' ', surround);
                fields[f] = fields[f].TrimEnd(surround);
            }

            if (fields.Length > attributeIndex) 
            {
                var key = fields[0];

                if (dictrionary.ContainsKey(key)) { continue; }

                var value = fields[attributeIndex];

                dictrionary.Add(key, value);
            }
        }

        return dictrionary;
    }

}
