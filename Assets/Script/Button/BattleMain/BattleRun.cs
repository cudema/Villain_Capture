using UnityEngine;

public class BattleRun : BattleMainButtonBase
{
    public override void Action()
    {
        BattleManager.battlemanager.PlayerAction((int)BattleAction.도주);
        BattleManager.battlemanager.runConttorl.BattleRunRandom();
    }
}
