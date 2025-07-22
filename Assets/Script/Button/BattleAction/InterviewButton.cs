using UnityEngine;

public class InterviewButton : BattleActionButtonBase
{
    public override void Action()
    {
        DialogueManager.instance.PrintDialogue(UIData.interview);

        BattleManager.PlayerAction((int)BattleAction.행동);
    }
}
