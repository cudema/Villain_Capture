using System.Collections;
using UnityEngine;

public class HackBullet : BulletBase
{
    Collider attackCollider;
    Renderer attackRenderer;

    bool isEnaged;
    bool isBoobm = false;

    [SerializeField]
    Material attack;
    [SerializeField]
    Material dilay;

    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        attackCollider = GetComponent<Collider>();
        isEnaged = patternBase.isEnaged;
        attackRenderer = GetComponent<Renderer>();
        ShootBullet();
    }

    private void Update()
    {
        
    }

    protected override void ShootBullet()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(attackDelay);

        attackCollider.enabled = true;
        attackRenderer.material = attack;
        Debug.Log(0);

        yield return new WaitForSeconds(attackDelay / 2);

        if (isEnaged)
        {
            attackCollider.enabled = false;
            attackRenderer.material = dilay;
            Debug.Log(1);

            yield return new WaitUntil(() => isBoobm);
            yield return new WaitForSeconds(attackDelay);
        }

        Destroy(gameObject);
    }

    public void SetBoobm()
    {
        isBoobm = true;
        attackCollider.enabled = true;
        attackRenderer.material = attack;
    }
}
