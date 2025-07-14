using System.Collections;
using UnityEngine;

public class DaggerBullet : BulletBase
{
    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
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

        while (true)
        {
            transform.position -= transform.right * speed * Time.deltaTime;
            if (Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 6)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
