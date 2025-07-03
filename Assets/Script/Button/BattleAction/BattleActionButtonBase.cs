using UnityEngine;

public class BattleActionButtonBase : BattleButtonBase
{
    [SerializeField]
    protected int thisAction = 0;
    GameObject selectArrow;

    private void Awake()
    {
        action = thisAction;
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
}
