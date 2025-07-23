using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitCollapses", menuName = "Scriptable Objects/SplitCollapses")]
public class SplitCollapses : PatternBase
{
    Vector3[] spawnVector = new Vector3[4];
    [Header("4���� ����")]
    [SerializeField]
    bool isFourWayAttack;

    List<int> ativeVector = new List<int>();

    public override void StartPattern()
    {
        if (isChangedFild)
        {
            BattleManager.battlemanager.ChangeFild(center, radius);
        }

        spawnVector[0] = new Vector3(BattleManager.battlemanager.Center.x + (BattleManager.battlemanager.Radius.x / 2) + 0.25f, BattleManager.battlemanager.Center.y, enemy.transform.position.z);
        spawnVector[1] = new Vector3(BattleManager.battlemanager.Center.x - (BattleManager.battlemanager.Radius.x / 2) - 0.25f, BattleManager.battlemanager.Center.y, enemy.transform.position.z);
        spawnVector[2] = new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y + (BattleManager.battlemanager.Radius.y / 2) + 0.25f, enemy.transform.position.z);
        spawnVector[3] = new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y - (BattleManager.battlemanager.Radius.y / 2) - 0.25f, enemy.transform.position.z);
        ativeVector.Clear();
        ativeVector.Add(0);
        ativeVector.Add(1);
        ativeVector.Add(2);
        ativeVector.Add(3);

        enemy.StartCoroutine(BingPattern());
    }

    protected override IEnumerator BingPattern()
    {
        if (isFourWayAttack)
        {
            int tempSpawn = -1;
            for (int i = 0; i < bulletCount; i++)
            {
                tempSpawn = GetRandomVector(tempSpawn);
                go = Instantiate(bullet, spawnVector[tempSpawn], Quaternion.identity, bulletParent);
                go.GetComponent<BulletBase>().Setup(this);

                yield return new WaitForSeconds(bulletSpawnDelay);
            }
        }
        else
        {
            int tempSpawn = Random.Range(0, spawnVector.Length);
            for (int i = 0; i < bulletCount; i++)
            {
                go = Instantiate(bullet, spawnVector[tempSpawn++ % 2], Quaternion.identity, bulletParent);
                go.GetComponent<BulletBase>().Setup(this);

                yield return new WaitForSeconds(bulletSpawnDelay);
            }
        }

        yield return new WaitUntil(() => go == null);

        BattleManager.battlemanager.ChangeTrun(Trun.아군);
    }

    int GetRandomVector(int currentVector = -1)
    {
        int temp = Random.Range(0, ativeVector.Count);
        int choce = ativeVector[temp];
        ativeVector.RemoveAt(temp);
        if (currentVector == -1)
        {
            return choce;
        }
        ativeVector.Add(currentVector);

        return choce;
    }
}
