using System.Collections;
using UnityEngine;

public class FractureBusterBullet : BulletBase
{
    GameObject warning;
    GameObject bullet;
    GameObject[] smallBullet = new GameObject[3];
    Vector2[] randomPos = new Vector2[3];

    float smallBulletArrivalTime;

    Vector2 pointOfBullet;

    public void Setup(FractureBuster patternBase)
    {
        attackDelay = patternBase.attackDelay;
        speed = patternBase.bulletDatas[0].speed;
        warning = transform.GetChild(0).gameObject;
        bullet = transform.GetChild(1).gameObject;
        smallBullet[0] = bullet.transform.GetChild(0).gameObject;
        smallBullet[1] = bullet.transform.GetChild(1).gameObject;
        smallBullet[2] = bullet.transform.GetChild(2).gameObject;

        for (int i = 0; i < patternBase.bulletDatas.Length; i++)
        {
            if (patternBase.bulletDatas[i].name == "돌조각(small)")
            {
                smallBullet[1].GetComponent<BulletAttack>().SetDamage(patternBase.bulletDatas[i].damage);
                smallBullet[2].GetComponent<BulletAttack>().SetDamage(patternBase.bulletDatas[i].damage);
                continue;
            }
            if (patternBase.bulletDatas[i].name == "돌조각(big)")
            {
                smallBullet[0].GetComponent<BulletAttack>().SetDamage(patternBase.bulletDatas[i].damage);
                continue;
            }
            if (patternBase.bulletDatas[i].name == "돌덩이")
            {
                bullet.GetComponent<BulletAttack>().SetDamage(patternBase.bulletDatas[i].damage);
                continue;
            }
        }

        pointOfBullet = new Vector2(BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x, BattleManager.battlemanager.Center.y);
        randomPos = patternBase.AtivePos;
        smallBulletArrivalTime = patternBase.SmallBulletArrivalTime;
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

        warning.SetActive(false);
        bullet.SetActive(true);

        while (bullet.transform.position.x > pointOfBullet.x)
        {
            bullet.transform.localPosition -= new Vector3(speed * Time.deltaTime, 0, 0);
            yield return null;
        }

        smallBullet[0].SetActive(true);
        smallBullet[1].SetActive(true);
        smallBullet[2].SetActive(true);
        bullet.GetComponent<MeshRenderer>().enabled = false;

        while (!Arrival())
        {
            smallBullet[0].transform.localPosition += (Vector2.Distance(pointOfBullet, randomPos[1])) * GoToPos(randomPos[1]) / smallBulletArrivalTime;
            smallBullet[1].transform.localPosition += (Vector2.Distance(pointOfBullet, randomPos[0])) * GoToPos(randomPos[0]) / smallBulletArrivalTime;
            smallBullet[2].transform.localPosition += (Vector2.Distance(pointOfBullet, randomPos[2])) * GoToPos(randomPos[2]) / smallBulletArrivalTime;
            yield return null;
        }
    }

    Vector3 GoToPos(Vector2 pos)
    {
        return (pos - pointOfBullet).normalized * Time.deltaTime;
    }

    public bool Arrival()
    {
        bool a1 = Vector2.Distance((Vector2)smallBullet[0].transform.position, randomPos[1]) < 0.1f;
        bool a2 = Vector2.Distance((Vector2)smallBullet[1].transform.position, randomPos[0]) < 0.1f;
        bool a3 = Vector2.Distance((Vector2)smallBullet[2].transform.position, randomPos[2]) < 0.1f;
        return a1 && a2 && a3;
    }
}
