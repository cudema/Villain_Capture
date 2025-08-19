using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FearTheDarkness", menuName = "Scriptable Objects/FearTheDarkness")]
public class FearTheDarkness : PatternBase
{
    [Header("종료 딜레이")]
    [SerializeField]
    float endDelay;

    [Header("양초 설정")]
    [SerializeField]
    GameObject candlePrefab;
    [SerializeField]
    float spawnRadius;
    [SerializeField]
    float senseRadius;
    [SerializeField]
    float patternTime;
    public float SenseRadius { get => senseRadius; private set => senseRadius = value; }

    Vector2 spawnPosRange;
    Vector2[] spawnedBulletPos;
    Vector2[] spawnCandlePos = new Vector2[4];
    [HideInInspector]
    public CandleEffect candle;
    bool isFail = false;
    //임시
    [HideInInspector]
    public bool end = false;

    public override void SetPattern()
    {
        base.SetPattern();

        end = false;
        isFail = false;

        spawnedBulletPos = new Vector2[bulletCount];
        spawnPosRange = BattleManager.battlemanager.Center + BattleManager.battlemanager.Radius - (Vector2.one * spawnRadius) + (Vector2.one * 0.5f);

        spawnCandlePos[0] = BattleManager.battlemanager.Center + BattleManager.battlemanager.Radius;
        spawnCandlePos[1] = BattleManager.battlemanager.Center - BattleManager.battlemanager.Radius;
        spawnCandlePos[2] = BattleManager.battlemanager.Center + new Vector2(-BattleManager.battlemanager.Radius.x, BattleManager.battlemanager.Radius.y);
        spawnCandlePos[3] = BattleManager.battlemanager.Center + new Vector2(BattleManager.battlemanager.Radius.x, -BattleManager.battlemanager.Radius.y);
    }

    public override void StartPattern()
    {
        PlayerContoller.instance.transform.position = new Vector3(center.x, center.y, 1);
        enemy.StartCoroutine(BingPattern());
    }

    protected override IEnumerator BingPattern()
    {
        enemy.animator.SetTrigger("UP");
        InputManager.inputManager.ChangeBattleNonInput();

        yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("DustyBone_Back") && enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f);

        enemy.OffRenderer();
        for (int i = 0; i < bulletCount; i++)
        {
            go = Instantiate(bullet, GetRandomPosToBullet(), Quaternion.identity);
            go.GetComponent<BulletBase>().Setup(this);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }
        candle = Instantiate(candlePrefab, (Vector3)spawnCandlePos[Random.Range(0, spawnCandlePos.Length)] + new Vector3(0, 0, 1), Quaternion.identity).GetComponent<CandleEffect>();
        yield return candle.StartCoroutine(candle.VignetteEffect());
        InputManager.inputManager.ChangeBattleBeforeInput();
        float time = Time.time;
        yield return new WaitUntil(() => IsEndChack(time)); //조건 수정해서 시작 시 멈추는거 구현
        if (Time.time - time > patternTime)
        {
            EndEffect();
        }
        InputManager.inputManager.ChangeBattleNonInput();
        yield return new WaitUntil(() => candle == null);

        enemy.OnRenderer();
        enemy.animator.SetTrigger("Down");

        if (isFail)
        {
            enemy.transform.position = PlayerContoller.instance.transform.position + new Vector3(1.5f, 0, 0);

            yield return new WaitForSeconds(attackDelay);

            enemy.animator.Play("Attack1");

            yield return null;
            yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 18f / 24f);

            PlayerContoller.instance.GetComponent<IHealthReporter>().TakeDamage(enemyDamage);
        }

        yield return new WaitForSeconds(endDelay);

        StopPattern();
    }

    Vector3 GetRandomPosToBullet()
    {
        Vector2 temp;
        bool isFandPos = false;
        int tryCount = 0;
        do
        {
            float x = Random.Range(-spawnPosRange.x, spawnPosRange.x);
            float y = Random.Range(-spawnPosRange.y, spawnPosRange.y);
            temp = new Vector2(x, y);

            for (int i = 0; i < spawnedBulletPos.Length; i++)
            {
                if (Vector2.Distance(spawnedBulletPos[i], temp) < spawnRadius + senseRadius)
                {
                    break;
                }
                if (spawnedBulletPos[i] == Vector2.zero)
                {
                    isFandPos = true;
                    spawnedBulletPos[i] = temp;
                    break;
                }
            }
            tryCount++;
        } while (!isFandPos && tryCount < 500000);
        Debug.Log(tryCount);
        return new Vector3(temp.x, temp.y, enemy.transform.position.z);
    }

    public void EndEffect()
    {
        candle.EndEffect();
        isFail = true;
    }
    public override void StopPattern()
    {
        end = true;
        base.StopPattern();
    }

    bool IsEndChack(float time)
    {
        return candle == null || Time.time - time > patternTime;
    }
}
