using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "DaggerThrow", menuName = "Scriptable Objects/DaggerThrow")]
public class DaggerThrow : PatternBase
{
    Vector3[] spawnPos = new Vector3[3];

    public override void SetPattern()
    {
        base.SetPattern();
        float temp = BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y + 2;

        spawnPos[0] = new Vector3(BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x, temp, enemy.transform.position.z);
        spawnPos[1] = new Vector3(BattleManager.battlemanager.Center.x, temp, enemy.transform.position.z);
        spawnPos[2] = new Vector3(BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x, temp, enemy.transform.position.z);
    }

    public override void StartPattern()
    {
        enemy.StartCoroutine(BingPattern());
    }

    protected override IEnumerator BingPattern()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            enemy.animator.Play("Attack0");

            for (int j = 0; j < 3; j++)
            {
                go = Instantiate(bullet, spawnPos[j], Quaternion.identity, bulletParent);
                go.GetComponent<BulletBase>().Setup(this);
            }

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitUntil(() => go == null);

        StopPattern();
    }
}
