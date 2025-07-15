using System.Collections;
using UnityEngine;

public class DaggerDropBullet : BulletBase
{
    GameObject warning;
    GameObject bullet;

    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        warning = transform.GetChild(0).gameObject;
        bullet = transform.GetChild(1).gameObject;
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
        if (transform.position.y > BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }

        yield return new WaitForSeconds(attackDelay);

        warning.SetActive(false);

        while (bullet != null)
        {
            bullet.transform.localPosition -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (bullet.transform.position.x < -10 || bullet.transform.position.y < -6)
            {
                Destroy(bullet);
            }
            yield return null;
        }

        Destroy(gameObject);
    }
}
