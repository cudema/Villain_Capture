using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected float speed;
    protected float attackDelay;

    public virtual void Setup(PatternBase patternBase)
    {
        speed = patternBase.bulletSpeed;
        attackDelay = patternBase.attackDelay;
    }

    private void Update()
    {
        ShootBullet();
    }

    protected virtual void ShootBullet()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    }
}
