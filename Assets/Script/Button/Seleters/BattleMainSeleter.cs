using UnityEngine;

public enum BattleAction { 초기화 = -1, 촬영 = 0, 행동, 아이템, 도주 }

public class BattleMainSeleter : BattleSeleterBase
{
    private void Start()
    {
        //이거 전투 시작시 셋팅하는 부분으로 움겨야함
        BattleManager.OnPlayerAction += OffUI;
        BattleManager.OnEnemyTrun += OffUI;
        BattleManager.OnPlayerTrun += OnUI;
        BattleManager.OnPlayerTrun += ResetSelecter;
        ResetSelecter();
    }

    public override void ChangeBattleAction(int newAction)
    {
        if ((BattleAction)currentAction != BattleAction.초기화)
        {
            buttons[currentAction].UnselectedThis();
        }
        currentAction = newAction;

        buttons[currentAction].SelectThis();
    }

    public override void ChangeBattleAction(Vector2 value)
    {
        if ((currentAction + (int)value.x) < (int)BattleAction.촬영 || (currentAction + (int)value.x) > (int)BattleAction.도주)
        {
            return;
        }

        if ((BattleAction)currentAction != BattleAction.초기화)
        {
            buttons[currentAction].UnselectedThis();
        }

        currentAction = (currentAction + (int)value.x);

        buttons[currentAction].SelectThis();
    }

    void ResetSelecter()
    {
        InputManager.ChangeSelecter(this);
    }
}
