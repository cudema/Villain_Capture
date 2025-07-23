using UnityEngine;

public class BattleFilming : BattleMainButtonBase
{
    [Header("�Կ�")]
    [SerializeField]
    SpawnObject temp;
    [SerializeField]
    NodePattern samplePattern;
    [SerializeField]
    PlayerContoller player;

    public override void Action()
    {
        BattleManager.battlemanager.PlayerAction((int)BattleAction.촬영);
    }

    public override void SelectThis()
    {
        base.SelectThis();
        BattleManager.OnPlayerAction += player.StartPattern;
    }

    public override void UnselectedThis()
    {
        base.UnselectedThis();
        BattleManager.OnPlayerAction -= player.StartPattern;
    }
}
