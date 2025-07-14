using System.Collections;
using UnityEngine;

public class DaggerUBullet : BulletBase
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
        while (attackDelay > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(transform.position.y - PlayerContoller.instance.transform.position.y, transform.position.x - PlayerContoller.instance.transform.position.x) * Mathf.Rad2Deg);

            attackDelay -= Time.deltaTime;
            yield return null;
        }


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
