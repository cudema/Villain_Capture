using UnityEngine;

public class SenseDagger : BulletBase
{
    FearTheDarkness temp;
    SphereCollider senseRange;

    void Update()
    {
        if (temp.end)
        {
            Destroy(gameObject);
        }
    }

    public override void Setup(PatternBase patternBase)
    {
        speed = patternBase.bulletDatas[0].speed;
        attackDelay = patternBase.attackDelay;
        temp = (FearTheDarkness)patternBase;
        senseRange = GetComponent<SphereCollider>();
        senseRange.radius = temp.SenseRadius;
    }

    protected override void ShootBullet()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            temp.EndEffect();
        }
    }
}
