using UnityEngine;

public enum ItemType { 부품 = 0, 도구, 단서, 회복 }

[System.Serializable]
public class ItemData
{
    public string itemID;
    public ItemType type;
    public int count = 0;
}