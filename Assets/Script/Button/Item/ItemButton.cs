using TMPro;
using UnityEngine;

public class ItemButton : BattleButtonBase
{
    [SerializeField]
    int thisAction = 0;
    [SerializeField]
    PrintItemData printer;
    [SerializeField]
    PrintDialogue printDialogue;
    GameObject selectArrow;

    TextMeshProUGUI uiText;
    ItemData itemData;

    private void Awake()
    {
        action = thisAction;
        uiText = transform.GetComponentInChildren<TextMeshProUGUI>();
        selectArrow = transform.GetChild(1).gameObject;
    }

    public override void SelectThis()
    {
        selectArrow.SetActive(true);
        printer.SetItemData(itemData);
    }

    public override void UnselectedThis()
    {
        selectArrow.SetActive(false);
    }

    public override void Setup(ItemData itemData)
    {
        gameObject.SetActive(true);
        this.itemData = itemData;
        uiText.text = itemData.name;
    }

    public override void Action()
    {
        itemData.UseItem();
        printDialogue.transform.parent.gameObject.SetActive(true);
        printDialogue.PrintItem(itemData);
        BattleManager.battlemanager.PlayerAction((int)BattleAction.아이템);
    }
}
