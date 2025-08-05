using System.Linq;
using UnityEngine;

public class BattleItemSeleter : BattleSeleterBase
{
    [SerializeField]
    BattleSeleterBase BeforeSeleter;
    [SerializeField]
    protected string enemyUIGroupID;
    [SerializeField]
    UIAnimation uiAnimation;
    [SerializeField]
    PrintItemData printItemData;

    private void Start()
    {
        currentAction = 0;
        buttons[currentAction].SelectThis();
    }

    void OnEnable()
    {
        BattleManager.OnPlayerAction += OffUI;
        BattleManager.OnEnemyTrun += OffUI;
    }

    void OnDisable()
    {
        BattleManager.OnPlayerAction -= OffUI;
        BattleManager.OnEnemyTrun -= OffUI;
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
            BeforeSeleter.OnUI();
            InputManager.ChangeSelecter(BeforeSeleter);
            OffUI();
        }
    }

    public void OnItemData()
    {
        printItemData.gameObject.SetActive(true);
    }

    public void OffItemData()
    {
        printItemData.gameObject.SetActive(false);
    }

    public override void OnUI()
    {
        base.OnUI();
        uiAnimation.PlayEventUpAnimation();
        SetButton();
    }

    public override void OffUI()
    {
        base.OffUI();
        OffItemData();
    }

    void SetButton()
    {
        int a = 0;
        foreach (ItemData i in ItemCSVLoader.healItemCSV)
        {
            if (i.CurrentCount > 0)
            {
                buttons[a++].Setup(i);
            }
        }

        for (int i = a; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }
}
