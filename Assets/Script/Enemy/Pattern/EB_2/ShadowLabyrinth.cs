using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ShadowLabyrinth", menuName = "Scriptable Objects/ShadowLabyrinth")]
public class ShadowLabyrinth : PatternBase
{
    [Header("분신 설정")]
    [SerializeField]
    int spawnFakeEnemyCount;
    [SerializeField]
    float delay;

    [SerializeField]
    float endDelay;

    FakeEnemyBullet[] fakeEnemy;

    public override void SetPattern()
    {
        base.SetPattern();

        fakeEnemy = new FakeEnemyBullet[spawnFakeEnemyCount];
    }

    public override void StartPattern()
    {
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
            fakeEnemy[i].LockAt(i);
            fakeEnemy[i].OffEffect();

            tempSeta += 360 / spawnFakeEnemyCount;
        }

        yield return new WaitForSeconds(bulletSpawnDelay);

        for (int i = 0; i < bulletCount; ++i)
        {
            int tempRandom = Random.Range(0, fakeEnemy.Length);

            //enemy.transform.position = fakeEnemy[tempRandom].transform.position;
            //fakeEnemy[tempRandom].gameObject.SetActive(false);
            //enemy.OnRenderer();
            //enemy.OnWraning();
            for (int j = 0; j < fakeEnemy.Length; ++j)
            {
                if (j == tempRandom)
                {
                    continue;
                }
                fakeEnemy[j].OnEffect();
            }

            yield return new WaitForSeconds(attackDelay);

            //enemy.OffWraning();

            for (int j = 0; j < fakeEnemy.Length; ++j)
            {
                fakeEnemy[j].OffEffect();
                if (j == tempRandom)
                {
                    fakeEnemy[j].OnFakeAttack();
                    continue;
                }
                fakeEnemy[j].OnAttack();
            }

            yield return new WaitForSeconds(delay);

            //fakeEnemy[tempRandom].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(endDelay);

        enemy.OnRenderer();

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
