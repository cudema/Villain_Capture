using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SoulRelease", menuName = "Scriptable Objects/SoulRelease")]
public class SoulRelease : PatternBase
{
    [Header("돌진")]
    [SerializeField]
    float rushDelay;
    [SerializeField]
    float rushSpeed;
    [SerializeField]
    float attackRadius;

    public override void StartPattern()
    {
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        int count = 3;

        for (int i = 0; i < bulletCount; i++)
        {
            float rotate = -15;
            for (int j = 0; j < count; j++)
            {
                go = Instantiate(bullet, enemy.transform.position, Quaternion.Euler(new Vector3(0, 0, rotate)));
                go.GetComponent<BulletBase>().Setup(this);

                rotate += 30 / (count - 1);
            }

            if (count == 3)
            {
                count++;
            }
            else
            {
                count--;
            }

                yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitForSeconds(rushDelay);

        Vector3 player = PlayerContoller.instance.transform.position;

        while (Vector3.Distance(player, enemy.transform.position) > 0.1f)
        {
            enemy.transform.position += (player - enemy.transform.position).normalized * rushSpeed * Time.deltaTime;

            yield return null;
        }

        enemy.OnWraning();
        enemy.SetWraningScale(attackRadius * 2);
        if (!isEnaged)
        {
            enemy.OnParringable();
        }

        yield return new WaitForSeconds(attackDelay);

        enemy.OffParringable();
        enemy.OffWraning();
        enemy.SetWraningScale(1.5f);

        yield return new WaitForSeconds(attackDelay);

        BattleManager.ChangeTrun(Trun.아군);
    }
}
