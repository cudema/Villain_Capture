using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleActionSeleter : BattleSeleterBase
{
    [SerializeField]
    BattleSeleterBase BeforeSeleter;
    protected string enemyUIGroupID;
    [SerializeField]
    UIAnimation uiAnimation;

    private void Start()
    {
        currentAction = 0;
        buttons[currentAction].SelectThis();
        //SetButton();
    }

    public override void ChangeBattleAction(int newAction)
    {
        base.ChangeBattleAction(newAction);
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
            //Debug.Log(uiTexts[i].UIID);
        }
    }
}
