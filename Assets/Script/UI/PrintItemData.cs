using TMPro;
using UnityEngine;

public class PrintItemData : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI effect;
    [SerializeField]
    TextMeshProUGUI explanation;

    public void SetItemData(ItemData item)
    {
        itemName.text = item.name;
        effect.text = item.tooltip;
        explanation.text = item.description;
    }
}
