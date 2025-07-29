using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ChasingDagger", menuName = "Scriptable Objects/ChasingDagger")]
public class ChasingDagger : PatternBase
{
    [SerializeField]
    float dontSpawnRadius;

    float maxX;
    float minX;
    float maxY;
    float minY;

    public override void SetPattern()
    {
        base.SetPattern();

        maxX = BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x;
        minX = BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x;
        maxY = BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y;
        minY = BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y;
    }

    public override void StartPattern()
    {
        enemy.StartCoroutine(BingPattern());
    }

    protected override IEnumerator BingPattern()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 spawnPos = GetRandomPos();
            go = Instantiate(bullet, spawnPos, Quaternion.Euler(0, 0, Mathf.Atan2(spawnPos.y - PlayerContoller.instance.transform.position.y, spawnPos.x - PlayerContoller.instance.transform.position.x) * Mathf.Rad2Deg), bulletParent);
            go.GetComponent<BulletBase>().Setup(this);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitUntil(() => go == null);

        StopPattern();
    }

    Vector3 GetRandomPos()
    {
        Vector3 temp;
        while (true)
        {
            float ranX = Random.Range(minX, maxX);
            float ranY = Random.Range(minY, maxY);
            temp = new Vector3(ranX, ranY, enemy.transform.position.z);
            if (Vector3.Distance(PlayerContoller.instance.transform.position, temp) > dontSpawnRadius)
            {
                break;
            }
        }

        return temp;
    }
}
