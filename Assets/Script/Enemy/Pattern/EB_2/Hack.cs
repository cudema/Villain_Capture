using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Hack", menuName = "Scriptable Objects/Hack")]
public class Hack : PatternBase
{
    public override void SetPattern()
    {
        base.SetPattern();
    }

    public override void StartPattern()
    {
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            int temp = Random.Range(0, 3);
            enemy.animator.speed = 2.5f;
            enemy.animator.Play($"Attack{temp}");
            Vector3 playerPos = PlayerContoller.instance.transform.position;
            int ranRotation = Random.Range(0, 180);

            go = Instantiate(bullet, playerPos, Quaternion.Euler(0, 0, ranRotation), bulletParent);
            go.GetComponent<BulletBase>().Setup(this);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }
        enemy.animator.speed = 1.0f;
        if (isEnaged)
        {
            yield return new WaitForSeconds(attackDelay + attackDelay / 2);

            foreach (HackBullet a in bulletParent.GetComponentsInChildren<HackBullet>())
            {
                a.SetBoobm();
            }
        }

        yield return new WaitUntil(() => go == null);

        BattleManager.battlemanager.ChangeTrun(Trun.아군);

        yield return null;
    }
}
