using System.Collections.Generic;
using UnityEngine;

public class Inventori : MonoBehaviour
{
    List<ItemData> items;

    public void AddItem(ItemData newItem)
    {
        foreach (ItemData i in items)
        {
            // if (i.itemID == newItem.itemID)
            // {
            //     i.count += newItem.count;
            //     return;
            // }
        }
        items.Add(newItem);
    }

    public void UseItem(ItemData item)
    {
        foreach (ItemData i in items)
        {
            // if (i.itemID == item.itemID)
            // {
            //     return;
            // }
        }

        Debug.Log("아이템이 없습니다.");
    }
}