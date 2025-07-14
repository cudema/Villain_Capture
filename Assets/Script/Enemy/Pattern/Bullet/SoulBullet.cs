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
        if (Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 6)
        {
            Destroy(gameObject);
        }
    }
}
