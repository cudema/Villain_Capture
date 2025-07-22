using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using UnityEngine;

public class EnemyDialogue
{
    [Name("인터뷰 ID")]
    public string interviewID { get; set; }
    [Name("대사 그룹 ID")]    
    public string dialogueGrup { get; set; }
    [Name("대사 출력 순서")]
    public int? count { get; set; }
    [Name("화자")]
    public string speaker { get; set; }
    [Name("화자 감정 상태")]
    public string speakerEmotion { get; set; }
    [Name("대사 번호")]
    public int? dialogueNumber { get; set; }
    [Name("호감도 증감")]
    public int? emotionalGauge { get; set; }
    [Name("대사 텍스트_KR")]
    public string dialogueText { get; set; }
    [Name("다음 대사 그룹 ID")]
    public string nextDialogueGrup { get; set; }
    public bool isUse;
}

public class TempTextLoad
{
    static List<EnemyDialogue> tempCSV;
    static TextAsset tempAsset;

    public static void SetupText()
    {
        tempAsset = Resources.Load<TextAsset>("적_대사");
        StringReader reader = new StringReader(tempAsset.text);
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        IEnumerable<EnemyDialogue> records = csv.GetRecords<EnemyDialogue>();
        tempCSV = new List<EnemyDialogue>(records);
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

    public static EnemyDialogue GetEnemyDialogue(string groupID, Emotion emotion)
    {
        List<EnemyDialogue> group = new List<EnemyDialogue>();

        foreach (EnemyDialogue i in tempCSV)
        {
            if (i.dialogueGrup == groupID)
            {
                group.Add(i);
            }
        }

        for (int i = 0; i < group.Count; i++)
        {
            if (group[i].speakerEmotion == emotion.ToString() && !group[i].isUse)
            {
                group[i].isUse = true;
                return group[i];
            }
        }

        return null;
    }

}
