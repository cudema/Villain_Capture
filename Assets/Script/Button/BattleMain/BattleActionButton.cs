using UnityEngine;

public class BattleActionButton : BattleMainButtonBase
{
    [SerializeField]
    BattleActionSeleter actionSeleter;

    public override void SelectThis()
    {
        base.SelectThis();
    }

    public override void UnselectedThis()
    {
        base .UnselectedThis();
    }

    public override void Action()
    {
        actionSeleter.SetUIGroupName(BattleManager.battlemanager.CurrentEnemy.GetDialogueID());
        actionSeleter.OnUI();
        seleter.OffUI();
        base.Action();
        InputManager.ChangeSelecter(actionSeleter);
    }
}
