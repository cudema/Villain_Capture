using System.Collections;
using UnityEngine;

public class Ruby : EnemyBase
{
    private void Start()
    {
        Setup();
    }

    protected override IEnumerator PositionReset()
    {
        animator.SetTrigger("EndPattern");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("아마튜어_Ruby_Land"));
        transform.position = startPos;
    }
}
