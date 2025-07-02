using UnityEngine;

public class BattleFilming : BattleButtonBase
{
    [Header("√‘øµ")]
    [SerializeField]
    SpawnObject temp;
    [SerializeField]
    NodePattern samplePattern;
    [SerializeField]
    PlayerContoller player;

    public override void Action()
    {
        BattleManager.PlayerAction(BattleAction.√‘øµ);
    }

    public override void SelectThis()
    {
        base.SelectThis();
        BattleManager.OnPlayerAction += player.SetPattern;
    }

    public override void UnselectedThis()
    {
        base.UnselectedThis();
        BattleManager.OnPlayerAction -= player.SetPattern;
    }

    void Shoot()
    {

    }
}
