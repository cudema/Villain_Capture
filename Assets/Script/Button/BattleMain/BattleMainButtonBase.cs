using UnityEngine;
using UnityEngine.UI;

public class BattleMainButtonBase : BattleButtonBase
{
    [SerializeField]
    protected BattleAction thisAction;

    private void Awake()
    {
        seleter = transform.GetComponentInParent<BattleSeleterBase>();
        image = GetComponent<Image>();
        action = (int)thisAction;
    }
}
