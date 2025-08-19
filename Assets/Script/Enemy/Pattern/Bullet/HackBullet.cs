using System.Collections;
using UnityEngine;

public class HackBullet : BulletBase
{
    Collider attackCollider;
    Renderer attackRenderer;

    bool isEnaged;
    bool isBoobm = false;

    [SerializeField]
    GameObject attack;
    [SerializeField]
    GameObject dilay;
    [SerializeField]
    GameObject warning;
    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        attackCollider = GetComponent<Collider>();
        isEnaged = patternBase.isEnaged;
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
        warning.SetActive(false);
        attack.SetActive(true);


        yield return new WaitForSeconds(attackDelay / 2);

        if (isEnaged)
        {
            attack.SetActive(false);
            dilay.SetActive(true);
            attackCollider.enabled = false;

            yield return new WaitUntil(() => isBoobm);
            yield return new WaitForSeconds(attackDelay);
        }

        Destroy(gameObject);
    }

    public void SetBoobm()
    {
        isBoobm = true;
        attackCollider.enabled = true;
        attack.SetActive(true);
    }
}
