using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected float speed;

    public virtual void Setup(float speed)
    {
        this.speed = speed;
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
