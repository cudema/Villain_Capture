using System.Collections;
using UnityEngine;

public class BladeWaveBullet : BulletBase
{
    GameObject warning;
    GameObject bullet;

    public override void Setup(float speed, float atteckDelay)
    {
        base.Setup(speed, atteckDelay);
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
        while (attackDelay > 0)
        {
            Vector3 player = PlayerData.player.transform.position;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(transform.position.y - player.y, transform.position.x - player.x) * Mathf.Rad2Deg));
            attackDelay -= Time.deltaTime;

            yield return null;
        }

        warning.SetActive(false);

        while (bullet != null)
        {
            bullet.transform.localPosition -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (bullet.transform.position.x < -10)
            {
                Destroy(bullet);
            }
            yield return null;
        }

        Destroy(gameObject);
    }
}
