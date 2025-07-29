using UnityEngine;

public class BattleFilming : BattleMainButtonBase
{
    [Header("촬영")]
    [SerializeField]
    SpawnObject temp;
    [SerializeField]
    NodePattern samplePattern;

    public override void Action()
    {
        base.Action();
        BattleManager.battlemanager.PlayerAction((int)BattleAction.촬영);
    }

    public override void SelectThis()
    {
        base.SelectThis();
        BattleManager.OnPlayerAction += PlayerContoller.instance.StartPattern;
    }

    public override void UnselectedThis()
    {
        base.UnselectedThis();
        BattleManager.OnPlayerAction -= PlayerContoller.instance.StartPattern;
    }
}
