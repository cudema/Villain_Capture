using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class EnemyDialogue
{
    public string interviewID;
    public string dialogueGrup;
    public string speaker;
    public string speakerEmotion;
    public int emotionalGauge;
    public string dialogueText;
    public string nextDialogueGrup;
    public bool isUse;

    public void SetText(string[] text)
    {
        interviewID = text[0];
        dialogueGrup = text[1];
        speaker = text[3];
        speakerEmotion = text[4];
        int.TryParse(text[6], out emotionalGauge);
        dialogueText = text[7];
        nextDialogueGrup = text[8];
        isUse = false;
    }
}

public class TempTextLoad
{
    static List<EnemyDialogue> tempCSV;
    static TextAsset tempAsset;

    public static void SetupText()
    {
        tempCSV = new List<EnemyDialogue>();
        tempAsset = Resources.Load<TextAsset>("더스티 베일_인터뷰 대사.CSV");
        string csvText = Encoding.GetEncoding(949).GetString(tempAsset.bytes);
        StringReader reader = new StringReader(csvText);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            string[] fields = line.Split(',');

            EnemyDialogue tempDialogue = new EnemyDialogue();
            tempDialogue.SetText(fields);
            tempCSV.Add(tempDialogue);
        }
    }

    public static EnemyDialogue GetNextDialogue(string ID)
    {
        string temp = GetEnemyDialogue(ID).nextDialogueGrup;

        if (temp == "END")
        {
            return null;
        }

        List<EnemyDialogue> grup = new List<EnemyDialogue>();
        int tempIndex = temp.IndexOf("_Router");
        if (tempIndex > 0)
        {
            temp = temp.Replace("_Router", "");
            Debug.Log(temp);
            Debug.Log(0);
            foreach (EnemyDialogue i in tempCSV)
            {
                if (i.dialogueGrup == temp)
                {
                    Debug.Log(1);
                    grup.Add(i);
                }
            }
        }

        for (int i = 0; i < grup.Count; i++)
        {
            if (!grup[i].isUse)
            {
                grup[i].isUse = true;
                Debug.Log(2);
                return grup[i];
            }
        }

        return null;
    }

    public static EnemyDialogue GetNextDialogue(string ID, Emotion emotion)
    {
        string temp = GetEnemyDialogue(ID).nextDialogueGrup;

        if (temp == "END")
        {
            return null;
        }
        List<EnemyDialogue> grup = new List<EnemyDialogue>();
        int tempIndex = temp.IndexOf("_Router");
        if (tempIndex > 0)
        {
            temp = temp.Replace("_Router", "");
            Debug.Log(temp);
            Debug.Log(0);
            foreach (EnemyDialogue i in tempCSV)
            {
                if (i.dialogueGrup == temp)
                {
                    Debug.Log(1);
                    grup.Add(i);
                }
            }
        }

        for (int i = 0; i < grup.Count; i++)
        {
            if (grup[i].speakerEmotion == emotion.ToString() && !grup[i].isUse)
            {
                grup[i].isUse = true;
                Debug.Log(2);
                return grup[i];
            }
        }

        return null;
    }

    public static EnemyDialogue GetEnemyDialogue(string ID)
    {
        for (int i = 0; i < tempCSV.Count; i++)
        {
            if (ID == tempCSV[i].interviewID)
            {
                return tempCSV[i];
            }
        }

        Debug.LogError("존재하지 않는 ID");
        return new EnemyDialogue();
    }
}
