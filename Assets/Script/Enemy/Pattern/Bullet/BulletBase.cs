using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected float speed;
    protected float attackDelay;

    public virtual void Setup(float atteckDelay)
    {
        this.attackDelay = atteckDelay;
    }

    public virtual void Setup(float speed, float atteckDelay)
    {
        this.speed = speed;
        this.attackDelay = atteckDelay;
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
