using UnityEngine;

public class InterviewButton : BattleActionButtonBase
{
    public override void Action()
    {
        DialogueManager.instance.PrintDialogue(UIData.interview);

        BattleManager.battlemanager.PlayerAction((int)BattleAction.행동);
    }
}
