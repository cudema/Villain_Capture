using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected float speed;
    protected float attackDelay;
    protected float damage;

    public virtual void Setup(PatternBase patternBase)
    {
        speed = patternBase.bulletDatas[0].speed;
        attackDelay = patternBase.attackDelay;
        GetComponent<BulletAttack>().SetDamage(patternBase.bulletDatas[0].damage);
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
