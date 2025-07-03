using System.Collections;
using UnityEngine;

public class TwinCliveBullet : BulletBase
{
    Collider hitcollider;

    public override void Setup(float speed)
    {
        base.Setup(speed);
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
        yield return new WaitForSeconds(speed);

        hitcollider.enabled = true;

        yield return null; //공격 모션에 맞게 딜레이 추가

        Destroy(gameObject);
    }
}
