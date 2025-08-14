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
        //attackRenderer.material = attack;
        attackRenderer.enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(attackDelay / 2);

        if (isEnaged)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            attackRenderer.enabled = true;
            attackCollider.enabled = false;
            attackRenderer.material = dilay;

            yield return new WaitUntil(() => isBoobm);
            yield return new WaitForSeconds(attackDelay);
        }

        Destroy(gameObject);
    }

    public void SetBoobm()
    {
        isBoobm = true;
        attackRenderer.enabled = false;
        attackCollider.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
