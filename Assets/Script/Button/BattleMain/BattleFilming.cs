using UnityEngine;

public class BattleFilming : BattleMainButtonBase
{
    public override void Action()
    {
        BattleManager.battlemanager.PlayerAction((int)BattleAction.촬영);
        PlayerContoller.instance.StartPattern();
    }

    public override void SelectThis()
    {
        base.SelectThis();
        //BattleManager.OnPlayerAction += PlayerContoller.instance.StartPattern;
    }

    public override void UnselectedThis()
    {
        base.UnselectedThis();
        //BattleManager.OnPlayerAction -= PlayerContoller.instance.StartPattern;
    }
}
