using System.Data.SqlTypes;
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

    public void Setup(string[] csvLine)
    {
        id = csvLine[0];
        name = csvLine[1];
    }
}

public class HealItemData : ItemData
{
    public float heal;
    public float healPercent;
}

public class ProvisoItemData : ItemData
{

}

public class EquipmentItemData : ItemData
{

}

public class PartItemData : ItemData
{
    
}