using System.Collections;
using UnityEngine;

public class CutBullet : BulletBase
{
    Animator animator;

    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        animator = GetComponentInChildren<Animator>();
        ShootBullet();
    }

    private void Update()
    {

    }

    protected override void ShootBullet()
    {
        StartCoroutine(BigShoot());
    }

    IEnumerator BigShoot()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        Destroy(gameObject);
    }
}
