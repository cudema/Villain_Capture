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
        if ((currentAction + (int)value.x) < 0 || (currentAction + (int)value.x) > buttons.Count() - 1)
        {
            return;
        }

        if (currentAction != -1)
        {
            buttons[currentAction].UnselectedThis();
        }

        currentAction = (currentAction + (int)value.x);

        buttons[currentAction].SelectThis();
    }

    public void OnUI()
    {
        gameObject.SetActive(true);
    }

    public void OffUI()
    {
        gameObject.SetActive(false);
    }

    public void SelectButton()
    {
        buttons[currentAction].Action();
    }
}
