using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleInterviewSelecter : BattleActionSeleter
{
    [SerializeField]
    PrintDialogue enemyDialogue;

    InterviewButton[] interviewButtons;

    private void Awake()
    {
        interviewButtons = GetComponentsInChildren<InterviewButton>();
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
        Debug.Log(-1);
        yield return enemyDialogue.StartCoroutine(enemyDialogue.PrintTextCoroutine(interviewButtons[currentAction].interviewText));
        Debug.Log("³¡");
        yield return new WaitUntil(() => Input.anyKeyDown);

        enemyDialogue.ResetText();

        BattleManager.ChangeTrun(Trun.Àû);
    }

    public override void SelectButton()
    {
        interviewButtons[currentAction].Action();
    }
}
