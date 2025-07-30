using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleActionSeleter : BattleSeleterBase
{
    [SerializeField]
    BattleSeleterBase BeforeSeleter;
    [SerializeField]
    protected string enemyUIGroupID;
    [SerializeField]
    UIAnimation uiAnimation;

    private void Start()
    {
        BattleManager.OnPlayerAction += OffUI;
        BattleManager.OnEnemyTrun += OffUI;
        currentAction = 0;
        buttons[currentAction].SelectThis();
        SetButton();
    }

    public override void ChangeBattleAction(int newAction)
    {
        base.ChangeBattleAction(newAction);
    }

    public override void ChangeBattleAction(Vector2 value)
    {
        if (value.x < 0 && currentAction % 2 == 0)
        {
            return;
        }
        if (value.x > 0 && currentAction % 2 == 1)
        {
            return;
        }

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
            BeforeSeleter.ReturnUI();
            InputManager.ChangeSelecter(BeforeSeleter);
            OffUI();
        }
    }

    public override void OnUI()
    {
        base.OnUI();
        uiAnimation.PlayUpAnimation();
        SetButton();
    }

    public void SetUIGroupName(string UIGroup)
    {
        enemyUIGroupID = UIGroup;
    }

    protected virtual void SetButton()
    {
        List<uiText> uiTexts = UICSVLoader.GetUIGroup(enemyUIGroupID);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Setup(uiTexts[i]);
        }
    }
}
