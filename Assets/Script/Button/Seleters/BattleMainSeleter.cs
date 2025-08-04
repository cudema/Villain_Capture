using UnityEngine;

public enum BattleAction { 초기화 = -1, 촬영 = 0, 인터뷰, 아이템, 도주 }

public class BattleMainSeleter : BattleSeleterBase
{
    [SerializeField]
    MainUIAnimation MainUI;

    private void Start()
    {
        //임시로 해 둔 것 게임 시작 시로 옴겨야함
        BattleManager.OnPlayerAction += OffUI;
        BattleManager.OnEnemyTrun += OffUI;
        BattleManager.OnPlayerTrun += OnUI;
        BattleManager.OnPlayerTrun += ResetSelecter;
        ResetSelecter();
    }

    void OnDisable()
    {
        BattleManager.OnPlayerAction -= OffUI;
        BattleManager.OnEnemyTrun -= OffUI;
        BattleManager.OnPlayerTrun -= OnUI;
        BattleManager.OnPlayerTrun -= ResetSelecter;
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

    public override void OnUI()
    {
        MainUI.PlayUpAnimation();
    }

    public override void OffUI()
    {
        MainUI.PlayDownAnimation();
    }

    public override void ReturnUI()
    {
        MainUI.PlayUpAnimation();
    }

    void ResetSelecter()
    {
        InputManager.ChangeSelecter(this);
    }
}
