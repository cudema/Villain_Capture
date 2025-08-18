using System.Collections;
using UnityEngine;

public class FakeEnemyBullet : BulletBase
{
    [SerializeField]
    GameObject attackCollider;
    [SerializeField]
    GameObject model;
    [SerializeField]
    GameObject attack;
    [SerializeField]
    GameObject effect;

    Animator animator;

    private void Update()
    {

    }
    public override void Setup(PatternBase patternBase)
    {
        float temp = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
        attack.transform.rotation = Quaternion.Euler(new Vector3(0, 0, temp));
        animator = GetComponentInChildren<Animator>();
        base.Setup(patternBase);
    }

    public void LockAt(int index)
    {
        model.transform.localRotation = Quaternion.Euler(new Vector3(0, -90 + (index * -45), 0));
    }

    protected override void ShootBullet()
    {
        base.ShootBullet();
    }

    public void OnAttack()
    {
        StartCoroutine(Attack());
    }

    public void OnFakeAttack()
    {
        animator.SetTrigger("Attack");
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).IsName("DustyBone_DustyIdle"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 13f / 24f);
        attackCollider.SetActive(true);
        yield return null;
        attackCollider.SetActive(false);
    }

    public void OnEffect()
    {
        effect.SetActive(true);
    }

    public void OffEffect()
    {
        effect.SetActive(false);
    }
}
