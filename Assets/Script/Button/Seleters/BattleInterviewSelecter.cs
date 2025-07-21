using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleInterviewSelecter : BattleActionSeleter
{
    [SerializeField]
    PrintDialogue enemyDialogue;

    [SerializeField]
    InterviewButton[] interviewButtons;

    private void Awake()
    {

    }

    private void Start()
    {
        BattleManager.OnPlayerAction += OffUI;
        BattleManager.OnEnemyTrun += OffUI;
        currentAction = 0;
        interviewButtons[currentAction].SelectThis();
    }

    private void OnEnable()
    {
        BattleManager.OnPlayerAction += PrintDialogue;
    }

    private void OnDisable()
    {
        BattleManager.OnPlayerAction -= PrintDialogue;
    }

    public override void ChangeBattleAction(int newAction)
    {
        if (currentAction != -1)
        {
            interviewButtons[currentAction].UnselectedThis();
        }
        currentAction = newAction;

        interviewButtons[currentAction].SelectThis();
    }

    public override void ChangeBattleAction(Vector2 value)
    {
        int temp = (currentAction + (int)value.x) - (int)value.y * 2;
        if (temp < 0 || temp > interviewButtons.Count() - 1)
        {
            return;
        }

        if (currentAction != -1)
        {
            interviewButtons[currentAction].UnselectedThis();
        }

        currentAction = temp;

        interviewButtons[currentAction].SelectThis();
    }

    void PrintDialogue()
    {
        enemyDialogue.StartCoroutine(PrintAction());
    }

    IEnumerator PrintAction()
    {
        yield return enemyDialogue.StartCoroutine(enemyDialogue.PrintTextCoroutine(TempTextLoad.GetEnemyDialogue("ITV_001")));
        enemyDialogue.ResetText();

        BattleManager.ChangeTrun(Trun.적);
    }

    public override void SelectButton()
    {
        interviewButtons[currentAction].Action();
    }

    public override void OnUI()
    {
        interviewButtons = GetComponentsInChildren<InterviewButton>();
        gameObject.SetActive(true);
        SetButton();
    }

    public override void OffUI()
    {
        base.OffUI();
        for (int i = 0; i < interviewButtons.Length; i++)
        {
            interviewButtons[i].gameObject.SetActive(true);
        }
    }

    void SetButton()
    {
        List<uiText> uiTexts = UICSVLoader.GetUIGroup(enemyUIGroupID);
        for (int i = 0; i < uiTexts.Count; i++)
        {
            Debug.Log(0);
            interviewButtons[i].Setup(uiTexts[i]);
        }

        if (uiTexts.Count < interviewButtons.Length)
        {
            for (int i = uiTexts.Count; i < interviewButtons.Length; i++)
            {
                interviewButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
