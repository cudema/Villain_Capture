using System.Collections;
using UnityEngine;

public class TwinCliveBullet : BulletBase
{
    Collider hitcollider;

    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        hitcollider = GetComponent<Collider>();
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

        yield return null; //공격 모션에 맞게 딜레이 추가

        Destroy(gameObject);
    }
}
