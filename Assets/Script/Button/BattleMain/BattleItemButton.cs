using UnityEngine;

public class BattleItemButton : BattleMainButtonBase
{
    [SerializeField]
    BattleActionSeleter itemSeleter;

    public override void SelectThis()
    {
        base.SelectThis();
    }

    public override void UnselectedThis()
    {
        base.UnselectedThis();
    }

    public override void Action()
    {
        itemSeleter.OnUI();
        seleter.OffUI();
        base.Action();
        InputManager.ChangeSelecter(itemSeleter);
    }
}
