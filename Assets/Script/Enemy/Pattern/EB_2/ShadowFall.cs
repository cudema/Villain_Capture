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
            enemy.animator.SetTrigger("UP");

            yield return new WaitForSeconds(enemySpawnDelay);

            enemy.animator.SetBool("IsRush", true);
            enemy.transform.GetChild(2).rotation = Quaternion.Euler(new Vector3(90, 180, 0));

            enemy.transform.position = new Vector3(PlayerContoller.instance.transform.position.x, spawnY, enemy.transform.position.z);
            enemy.OnRenderer();

            yield return new WaitForSeconds(rushDelay);

            enemy.OnAttack();

            while (enemy.transform.position.y > floorY)
            {
                enemy.transform.position += Vector3.down * rushSpeed * Time.deltaTime;
                yield return null;
            }

            enemy.transform.GetChild(2).rotation = Quaternion.Euler(new Vector3(0, -130, 0));
            enemy.animator.SetBool("IsRush", false);

            go = Instantiate(bullet, enemy.transform.position + Vector3.left * 0.7f, Quaternion.identity, bulletParent);
            go.GetComponent<BulletBase>().Setup(this);
            go = Instantiate(bullet, enemy.transform.position + Vector3.right * 0.7f, Quaternion.Euler(new Vector3(0, 0, 180)), bulletParent);
            go.GetComponent<BulletBase>().Setup(this);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        enemy.animator.SetBool("IsRush", false);

        yield return new WaitForSeconds(bulletSpawnDelay);

        StopPattern();
    }
}
