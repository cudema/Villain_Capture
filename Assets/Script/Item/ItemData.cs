using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using Unity.Burst;
using UnityEditor.Rendering;
using UnityEngine;

public enum ItemType { 부품 = 0, 도구, 단서, 회복 }

[System.Serializable]
public class ItemData
{
    public string id;
    public string name;
    public int maxCount;
    public bool expendable;
    public string image;
    public string tooltip;
    public string description;

    int currentCount = 1;
    public int CurrentCount
    {
        get => currentCount;
        set
        {
            currentCount = Mathf.Clamp(value, 0, maxCount);
        }
    }

    public void Setup(string[] csvLine)
    {
        id = csvLine[0];
        name = csvLine[1];
        int.TryParse(csvLine[3], out maxCount);
        bool.TryParse(csvLine[4], out expendable);
        image = csvLine[5];
        tooltip = csvLine[6];
        description = csvLine[7];
    }

    public virtual void UseItem()
    {
        if (expendable)
        {
            CurrentCount--;
        }
    }
}

public class HealItemData : ItemData
{
    public float heal;
    public float healPercent;

    public void Setup2(string[] csvLine)
    {
        float.TryParse(csvLine[4], out heal);
        float.TryParse(csvLine[5], out healPercent);
    }

    public override void UseItem()
    {
        base.UseItem();
        PlayerContoller.instance.HealPlayer(heal, healPercent);
        DialogueManager.instance.PrintItem(name, heal, healPercent);
    }
}

public class ProvisoItemData : ItemData
{
    public string unlockInterview;

    public void Setup2(string[] csvLine)
    {
        unlockInterview = csvLine[7];
    }
}

public class EquipmentItemData : ItemData
{
    public int emotionalGauge;
    public string buff;
    public string naxtDialogueID;

    public void Setup2(string[] csvLine)
    {
        int.TryParse(csvLine[4], out emotionalGauge);
        buff = csvLine[6];
        naxtDialogueID = csvLine[9];
    }

    public override void UseItem()
    {
        base.UseItem();
        DialogueManager.instance.PrintItem(name, naxtDialogueID);
    }
}

public class PartItemData : ItemData
{
    public void Setup2(string[] csvLine)
    {

    }
}

public class ItemCSVLoader
{
    public static List<HealItemData> healItemCSV;
    public static List<ProvisoItemData> provisoItemCSV;
    public static List<EquipmentItemData> equipmentItemCSV;
    public static List<PartItemData> partItemCSV;
    static TextAsset itemAsset;
    static TextAsset itemEffectAsset;

    public static void SetItemCSV()
    {
        healItemCSV = new List<HealItemData>();
        provisoItemCSV = new List<ProvisoItemData>();
        equipmentItemCSV = new List<EquipmentItemData>();
        partItemCSV = new List<PartItemData>();
        itemAsset = Resources.Load<TextAsset>("아이템기본");
        itemEffectAsset = Resources.Load<TextAsset>("아이템효과");

        StringReader reader = new StringReader(itemAsset.text);
        StringReader effectReader = new StringReader(itemEffectAsset.text);
        string line;
        string effectLine;
        while ((line = reader.ReadLine()) != null && (effectLine = effectReader.ReadLine()) != null)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(effectLine))
            {
                continue;
            }
            string[] itemdata = line.Split(',');
            string[] effectdata = effectLine.Split(',');
            switch (itemdata[2])
            {
                case "Part":
                    PartItemData patemp = new PartItemData();
                    patemp.Setup(itemdata);
                    patemp.Setup2(effectdata);
                    partItemCSV.Add(patemp);
                    break;
                case "Equipment":
                    EquipmentItemData eqtemp = new EquipmentItemData();
                    eqtemp.Setup(itemdata);
                    eqtemp.Setup2(effectdata);
                    equipmentItemCSV.Add(eqtemp);
                    break;
                case "Proviso":
                    ProvisoItemData prtemp = new ProvisoItemData();
                    prtemp.Setup(itemdata);
                    prtemp.Setup2(effectdata);
                    provisoItemCSV.Add(prtemp);
                    break;
                case "Heal":
                    HealItemData hetemp = new HealItemData();
                    hetemp.Setup(itemdata);
                    hetemp.Setup2(effectdata);
                    healItemCSV.Add(hetemp);
                    break;
                default:
                    break;
            }
        }
    }

    public static ItemData GetItemData(string id)
    {
        if (id.Contains("I_CL") || id.Contains("I_CF"))
        {
            foreach (ItemData i in partItemCSV)
            {
                if (i.id == id)
                {
                    return i;
                }
            }
        }
        if (id.Contains("I_EQ"))
        {
            foreach (ItemData i in equipmentItemCSV)
            {
                if (i.id == id)
                {
                    return i;
                }
            }
        }
        if (id.Contains("I_PV"))
        {
            foreach (ItemData i in provisoItemCSV)
            {
                if (i.id == id)
                {
                    return i;
                }
            }
        }
        if (id.Contains("I_HI"))
        {
            foreach (ItemData i in healItemCSV)
            {
                if (i.id == id)
                {
                    return i;
                }
            }
        }

        Debug.LogWarning("해당하는 아이템을 찾지 못했습니다.");
        return null;
    }
}