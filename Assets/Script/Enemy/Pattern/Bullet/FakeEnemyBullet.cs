using UnityEngine;

public class FakeEnemyBullet : BulletBase
{
    GameObject attackCollider;

    private void Update()
    {
        
    }
    public override void Setup(PatternBase patternBase)
    {
        attackCollider = transform.GetChild(0).gameObject;
        float temp = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, temp));
        base.Setup(patternBase);
    }
    protected override void ShootBullet()
    {
        base.ShootBullet();
    }

    public void OnAttack()
    {
        attackCollider.SetActive(true);
    }

    public void OffAttack()
    {
        attackCollider.SetActive(false);
    }
}
