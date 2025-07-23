using UnityEngine;

public class InterviewButton : BattleActionButtonBase
{
    public override void Setup(uiText uiText)
    {
        UIData = uiText;
        if (UIData.selectedCount >= 2)
        {
            this.uiText.text = "(질문 고갈됨)";
        }
        else
        {
            this.uiText.text = UIData.UIText;
        }
    }

    public override void Action()
    {
        if (UIData.selectedCount >= 2)
        {
            DialogueManager.instance.PrintDialogue("ITV_151");
        }
        else
        {
            UIData.selectedCount++;
            DialogueManager.instance.PrintDialogue(UIData.interview);
        }

        BattleManager.battlemanager.PlayerAction((int)BattleAction.행동);
    }
}
