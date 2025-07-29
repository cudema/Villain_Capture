using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ShadowFall", menuName = "Scriptable Objects/ShadowFall")]
public class ShadowFall : PatternBase
{
    [Header("���")]
    [SerializeField]
    float enemySpawnDelay;
    [SerializeField]
    float rushDelay;
    [SerializeField]
    float rushSpeed;
    [SerializeField]
    float rushDamage;
    [SerializeField]
    int rushCount;

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
        float floorY = BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y;
        float spawnY = BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y + 2;

        for (int i = 0; i < rushCount; i++)
        {
            enemy.OffRenderer();

            yield return new WaitForSeconds(enemySpawnDelay);

            enemy.transform.position = new Vector3(PlayerContoller.instance.transform.position.x, spawnY, enemy.transform.position.z);
            enemy.OnRenderer();

            yield return new WaitForSeconds(rushDelay);

            while (enemy.transform.position.y > floorY)
            {
                enemy.transform.position += Vector3.down * rushSpeed * Time.deltaTime;
                yield return null;
            }
            go = Instantiate(bullet, enemy.transform.position + Vector3.left * 0.7f, Quaternion.identity, bulletParent);
            go.GetComponent<BulletBase>().Setup(this);
            go = Instantiate(bullet, enemy.transform.position + Vector3.right * 0.7f, Quaternion.Euler(new Vector3(0, 0, 180)), bulletParent);
            go.GetComponent<BulletBase>().Setup(this);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitForSeconds(bulletSpawnDelay);

        StopPattern();
    }
}
