using System.Linq;
using UnityEngine;

public class BattleSeleterBase : MonoBehaviour
{
    protected BattleButtonBase[] buttons;

    protected int currentAction = -1;

    private void Awake()
    {
        buttons = GetComponentsInChildren<BattleButtonBase>();
    }

    public virtual void ChangeBattleAction(int newAction)
    {
        if (currentAction != -1)
        {
            buttons[currentAction].UnselectedThis();
        }
        currentAction = newAction;

        buttons[currentAction].SelectThis();
    }

    public virtual void ChangeBattleAction(Vector2 value)
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
        if (temp < 0 || temp > buttons.Count() - 1 || !buttons[temp].gameObject.activeSelf)
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

    public virtual void OnUI()
    {
        gameObject.SetActive(true);
    }

    public virtual void OffUI()
    {
        gameObject.SetActive(false);
    }

    public virtual void ReturnUI()
    {
        gameObject.SetActive(true);
    }

    public virtual void SelectButton()
    {
        buttons[currentAction].Action();
    }
}
