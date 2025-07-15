using UnityEngine;

public class DaggerRainBullet : BulletBase
{
    protected override void ShootBullet()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
