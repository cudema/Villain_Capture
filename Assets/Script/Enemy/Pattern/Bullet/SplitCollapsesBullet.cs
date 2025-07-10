using System.Collections;
using UnityEngine;

public class SplitCollapsesBullet : BulletBase
{
    Vector3 bulletScale;
    GameObject wraning;
    GameObject hitbax;

    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        if (BattleManager.battlemanager.Center.y - transform.position.y == 0)
        {
            bulletScale = new Vector3(BattleManager.battlemanager.Radius.x + 0.5f, BattleManager.battlemanager.Radius.y * 2 + 1, 1);
        }
        else
        {
            bulletScale = new Vector3(BattleManager.battlemanager.Radius.x * 2 + 1, BattleManager.battlemanager.Radius.y + 0.5f, 1);
        }
        wraning = transform.GetChild(0).gameObject;
        hitbax = transform.GetChild(1).gameObject;
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
        wraning.transform.localScale = bulletScale;
        hitbax.transform.localScale = bulletScale;

        wraning.SetActive(true);

        yield return new WaitForSeconds(attackDelay);

        wraning.SetActive(false);
        hitbax.SetActive(true);

        yield return new WaitForSeconds(speed);

        Destroy(gameObject);

        yield return null;
    }
}
