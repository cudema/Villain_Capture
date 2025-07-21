using UnityEngine;

public class InterviewButton : BattleActionButtonBase
{
    public string interviewText;

    public override void Action()
    {
        int temp = Random.Range(-1, 2);
        BattleManager.CurrentEnemy.EmotionalGauge += temp;

        BattleManager.PlayerAction((int)BattleAction.행동);
    }
}
