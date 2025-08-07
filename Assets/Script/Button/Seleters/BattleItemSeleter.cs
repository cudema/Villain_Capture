using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

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

    List<ItemData[]> healItems = new List<ItemData[]>();
    List<ItemData[]> eqItems = new List<ItemData[]>();

    private void Start()
    {
        currentAction = 0;
        buttons[currentAction].SelectThis();
    }

    public override void ChangeBattleAction(int newAction)
    {
        base.ChangeBattleAction(newAction);
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
        InputManager.ChangeSelecter(null);
    }

    void SetButton()
    {
        healItems.Clear();
        eqItems.Clear();

        int a = 0;
        int temp = 0;
        foreach (ItemData i in ItemCSVLoader.healItemCSV)
        {
            if (healItems.Count < 1 + a / 6)
            {
                healItems.Add(new ItemData[6]);
            }
            if (i.CurrentCount > 0)
            {
                healItems[a][temp++] = i;
            }
            if (temp == 6)
            {
                a++;
                temp = 0;
            }
        }

        a = 0;
        temp = 0;

        foreach (ItemData i in ItemCSVLoader.equipmentItemCSV)
        {
            Debug.Log(i.CurrentCount);
            if (eqItems.Count < 1 + a / 6)
            {
                eqItems.Add(new ItemData[6]);
            }
            if (i.CurrentCount > 0)
            {
                eqItems[a][temp++] = i;
            }
            if (temp == 6)
            {
                a++;
                temp = 0;
            }
        }

        OpenButton(0, 0);
    }

    public void OpenButton(int kategori, int page)
    {
        int temp = 0;
        if (kategori == 0)
        {
            foreach (ItemData i in healItems[page])
            {
                if (i == null)
                {
                    break;
                }
                buttons[temp++].Setup(i);
            }
        }
        else
        {
            foreach (ItemData i in eqItems[page])
            {
                if (i == null)
                {
                    break;
                }
                buttons[temp++].Setup(i);
            }
        }

        for (int i = temp; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        ChangeBattleAction(0);
    }
}
