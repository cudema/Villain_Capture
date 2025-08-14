using System.Collections;
using UnityEngine;

public class TwinCliveBullet : BulletBase
{
    Collider hitcollider;
    Animator animator;
    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        hitcollider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
        ShootBullet();
    }

    private void Update()
    {
        
    }

    protected override void ShootBullet()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(attackDelay);

        hitcollider.enabled = true;
        GetComponent<Renderer>().enabled = false;
        animator.Play("TwinClive");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("TwinClive") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        Destroy(gameObject);
    }
}
