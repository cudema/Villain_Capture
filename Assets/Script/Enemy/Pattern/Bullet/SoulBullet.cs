using UnityEngine;

public class SoulBullet : BulletBase
{
    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
    }

    protected override void ShootBullet()
    {
        transform.position -= transform.right * speed * Time.deltaTime;
        if (transform.position.x < -6)
        {
            Destroy(gameObject);
        }
    }
}
