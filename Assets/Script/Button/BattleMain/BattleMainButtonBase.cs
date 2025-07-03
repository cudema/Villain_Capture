using UnityEngine;

public class BattleMainButtonBase : BattleButtonBase
{
    [SerializeField]
    protected BattleAction thisAction;

    private void Awake()
    {
        action = (int)thisAction;
    }
}
