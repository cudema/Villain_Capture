using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class RubyDropBullet : BulletBase
{
    bool isUXO = false;
    Collider attackRange;
    Vector3 goToPos;

    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        attackRange = GetComponentInChildren<Collider>();
    }

    public void Setup(int i)
    {
        float tempX = Random.Range(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Radius.x);
        float tempY = Random.Range(BattleManager.battlemanager.Center.y, BattleManager.battlemanager.Radius.y);
        switch (i)
        {
            case 0:
                goToPos = new Vector3(tempX, tempY, transform.position.z);
                break;
            case 1:
                goToPos = new Vector3(-tempX, tempY, transform.position.z);
                break;
            case 2:
                goToPos = new Vector3(-tempX, -tempY, transform.position.z);
                break;
            case 3:
                goToPos = new Vector3(tempX, -tempY, transform.position.z);
                break;
            default:
                break;
        }

        ShootBullet();
    }

    public void SetUXO()
    {
        isUXO = true;
    }
    void Update()
    {

    }

    protected override void ShootBullet()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        Vector3 temp = goToPos - transform.position;
        while (Vector3.Distance(goToPos, transform.position) > 0.1f)
        {
            transform.position += temp.normalized * Time.deltaTime * speed;
            yield return null;
        }

        yield return new WaitForSeconds(attackDelay);

        if (isUXO)
        {
            attackRange.gameObject.SetActive(false);
            yield break;
        }
        else
        {
            attackRange.enabled = true;
        }

        yield return null;

        Destroy(gameObject);
    }

    public void Boom()
    {
        attackRange.gameObject.SetActive(true);
        attackRange.enabled = true;
    }
}
