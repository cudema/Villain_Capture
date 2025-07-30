using TMPro;
using UnityEngine;

public class ItemButton : BattleButtonBase
{
    [SerializeField]
    int thisAction = 0;
    GameObject selectArrow;

    TextMeshProUGUI uiText;

    private void Awake()
    {
        action = thisAction;
        uiText = transform.GetComponentInChildren<TextMeshProUGUI>();
        selectArrow = transform.GetChild(1).gameObject;
    }

    public override void SelectThis()
    {
        selectArrow.SetActive(true);
    }

    public override void UnselectedThis()
    {
        selectArrow.SetActive(false);
    }
    
    public override void Action()
    {

    }
}
