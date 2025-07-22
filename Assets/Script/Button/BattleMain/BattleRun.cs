using UnityEngine;

public class BattleRun : BattleMainButtonBase
{
    public override void Action()
    {
        BattleManager.PlayerAction((int)BattleAction.도주);
        BattleManager.battlemanager.runConttorl.BattleRunRandom();
    }
}
