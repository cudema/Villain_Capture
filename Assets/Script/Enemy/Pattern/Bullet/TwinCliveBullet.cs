using System.Collections;
using UnityEngine;

public class TwinCliveBullet : BulletBase
{
    Collider hitcollider;
    Animator animator;
    ParticleSystem ps;
    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        hitcollider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
        ps?.gameObject.SetActive(false);
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
        if (animator != null)
        {
            animator.Play("TwinClive");
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("TwinClive") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        }
        if (ps != null)
        {
            ps.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.2f);
        }

        Destroy(gameObject);
    }
}
