using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleInterviewSelecter : BattleActionSeleter
{
    // [SerializeField]
    // PrintDialogue enemyDialogue;

    // private void OnEnable()
    // {
    //     BattleManager.OnPlayerAction += PrintDialogue;
    // }

    // private void OnDisable()
    // {
    //     BattleManager.OnPlayerAction -= PrintDialogue;
    // }

    // void PrintDialogue()
    // {
    //     enemyDialogue.StartCoroutine(PrintAction());
    // }

    // IEnumerator PrintAction()
    // {
    //     yield return enemyDialogue.StartCoroutine(enemyDialogue.PrintTextCoroutine(TempTextLoad.GetEnemyDialogue("ITV_001")));
    //     enemyDialogue.ResetText();

    //     BattleManager.ChangeTrun(Trun.적);
    // }

    public override void OnUI()
    {
        gameObject.SetActive(true);
        SetButton();
    }

    public override void OffUI()
    {
        base.OffUI();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
        InputManager.ChangeSelecter(null);
    }

    protected override void SetButton()
    {
        List<uiText> uiTexts = UICSVLoader.GetUIGroup(enemyUIGroupID);
        for (int i = 0; i < uiTexts.Count; i++)
        {
            buttons[i].Setup(uiTexts[i]);
        }

        if (uiTexts.Count < buttons.Length)
        {
            for (int i = uiTexts.Count; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
