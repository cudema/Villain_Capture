using UnityEngine;

public enum BattleAction { 초기화 = -1, 촬영 = 0, 행동, 아이템, 도주 }

public class BattleMainSeleter : MonoBehaviour
{
    [SerializeField]
    BattleAction currentAction = BattleAction.초기화;
    [SerializeField]
    BattleButtonBase[] buttons = new BattleButtonBase[4];

    private void Start()
    {
        //이거 전투 시작시 셋팅하는 부분으로 움겨야함
        BattleManager.OnPlayerAction += OffUI;
        BattleManager.OnPlayerTrun += OnUI;
    }

    public void ChangeBattleAction(BattleAction newAction)
    {
        if (currentAction != BattleAction.초기화)
        {
            buttons[(int)currentAction].UnselectedThis();
        }
        currentAction = newAction;

        buttons[(int)currentAction].SelectThis();
    }

    public void ChangeBattleAction(float value)
    {
        if ((int)((float)currentAction + value) < (int)BattleAction.촬영 || (int)((float)currentAction + value) > (int)BattleAction.도주)
        {
            return;
        }

        if (currentAction != BattleAction.초기화)
        {
            buttons[(int)currentAction].UnselectedThis();
        }

        currentAction = (BattleAction)((float)currentAction + value);

        buttons[(int)currentAction].SelectThis();
    }

    public void SelectButton()
    {
        buttons[(int)currentAction].Action();
    }

    void OnUI()
    {
        gameObject.SetActive(true);
    }

    void OffUI()
    {
        gameObject.SetActive(false);
    }
}
