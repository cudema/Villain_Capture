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
        animator.SetTrigger("EndPattern");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("DustyBone_Land"));
        transform.position = startPos;
    }
}
