using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrintItemData : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI effect;
    [SerializeField]
    TextMeshProUGUI explanation;
    [SerializeField]
    Image icon;

    public void SetItemData(ItemData item)
    {
        itemName.text = item.name;
        effect.text = item.tooltip;
        explanation.text = item.description;
        string temp = "Item/" + item.image;
        icon.sprite = Resources.Load<Sprite>(temp);
    }
}
