using System.Linq;
using UnityEngine;

public class BattleActionSeleter : BattleSeleterBase
{
    [SerializeField]
    BattleSeleterBase temp;

    private void Start()
    {
        BattleManager.OnPlayerAction += OffUI;
        BattleManager.OnEnemyTrun += OffUI;
        currentAction = 0;
        buttons[currentAction].SelectThis();
    }

    public override void ChangeBattleAction(int newAction)
    {
        base.ChangeBattleAction(newAction);
    }

    public override void ChangeBattleAction(Vector2 value)
    {
        int temp = (currentAction + (int)value.x) - (int)value.y * 2;
        if (temp < 0 || temp > buttons.Count() - 1)
        {
            return;
        }

        if (currentAction != -1)
        {
            buttons[currentAction].UnselectedThis();
        }

        currentAction = temp;

        buttons[currentAction].SelectThis();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            temp.OnUI();
            InputManager.ChangeSelecter(temp);
            OffUI();
        }
    }
}
