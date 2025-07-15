using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "ShadowLabyrinth", menuName = "Scriptable Objects/ShadowLabyrinth")]
public class ShadowLabyrinth : PatternBase
{
    [Header("¼¨µµ¿ì")]
    [SerializeField]
    int spawnFakeEnemyCount;
    [SerializeField]
    float delay;

    FakeEnemyBullet[] fakeEnemy;

    public override void StartPattern()
    {
        fakeEnemy = new FakeEnemyBullet[spawnFakeEnemyCount];

        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        float tempSeta = 0;
        enemy.OffRenderer();
        for (int i = 0;  i < spawnFakeEnemyCount; ++i)
        {
            Vector3 spawnPos = new Vector3(Mathf.Cos(tempSeta * Mathf.Deg2Rad), Mathf.Sin(tempSeta * Mathf.Deg2Rad), 0) * 4 + new Vector3(0, 0, enemy.transform.position.z);
            fakeEnemy[i] = Instantiate(bullet, spawnPos, Quaternion.identity).GetComponent<FakeEnemyBullet>();
            fakeEnemy[i].Setup(this);

            tempSeta += 360 / spawnFakeEnemyCount;
        }

        yield return new WaitForSeconds(bulletSpawnDelay);

        for (int i = 0; i < bulletCount; ++i)
        {
            int tempRandom = Random.Range(0, fakeEnemy.Length);

            enemy.transform.position = fakeEnemy[tempRandom].transform.position;
            fakeEnemy[tempRandom].gameObject.SetActive(false);
            enemy.OnRenderer();
            enemy.OnWraning();

            yield return new WaitForSeconds(attackDelay);

            enemy.OffWraning();

            for (int j = 0; j < fakeEnemy.Length; ++j)
            {
                fakeEnemy[j].OnAttack();
            }

            yield return new WaitForSeconds(delay);

            for (int j = 0; j < fakeEnemy.Length; ++j)
            {
                fakeEnemy[j].OffAttack();
            }

            yield return new WaitForSeconds(delay);

            fakeEnemy[tempRandom].gameObject.SetActive(true);
        }

        StopPattern();
    }

    public override void StopPattern()
    {
        for (int i = 0; i < fakeEnemy.Length; ++i)
        {
            Destroy(fakeEnemy[i].gameObject);
        }
        base.StopPattern();
    }
}
