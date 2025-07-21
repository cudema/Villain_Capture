using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct uiText
{
    public string UIID;
    public string UIGroup;
    public string UIText;
    public string nextUIGroup;
    public string interview;

    public void Setup(string[] csvLine)
    {
        UIID = csvLine[0];
        UIGroup = csvLine[1];
        UIText = csvLine[2];
        nextUIGroup = csvLine[3];
        interview = csvLine[4];
    }
}

public class UICSVLoader
{
    static List<uiText> uiCSV;
    static TextAsset uiAsset;

    public static void SetUICSV()
    {
        uiCSV = new List<uiText>();
        uiAsset = Resources.Load<TextAsset>("인터뷰UI.CSV");
        StringReader reader = new StringReader(uiAsset.text);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            string[] temp = line.Split(',');

            uiText tempUI = new uiText();
            tempUI.Setup(temp);
            uiCSV.Add(tempUI);
        }
    }

    public static List<uiText> GetUIGroup(string UIGroup)
    {
        List<uiText> temp = new List<uiText>();

        string groupID = UIGroup;
        if (groupID.Contains("_Router"))
        {
            Debug.Log(groupID);
            groupID = groupID.Replace("_Router", "");
            Debug.Log(groupID);
        }

        foreach (uiText i in uiCSV)
        {
            Debug.Log(123);
            if (i.UIGroup == groupID)
            {
                Debug.Log(123132);
                temp.Add(i);
            }
        }

        return temp;
    }
}
