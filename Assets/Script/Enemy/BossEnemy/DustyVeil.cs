using System.Collections;
using UnityEngine;

public class DustyVeil : EnemyBase
{
    private void Start()
    {
        Setup();
    }

    protected override IEnumerator PositionReset()
    {
        return base.PositionReset();
    }
}
