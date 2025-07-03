using UnityEngine;

public class BattleFilming : BattleMainButtonBase
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
        BattleManager.PlayerAction((int)BattleAction.√‘øµ);
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
