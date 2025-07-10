using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "RageAssault", menuName = "Scriptable Objects/RageAssault")]
public class RageAssault : PatternBase
{
    [Header("보스 돌진")]
    [SerializeField]
    float rushDelay;
    [SerializeField]
    float rushSpeed;

    [Header("보스 찍기")]
    [SerializeField]
    float chopDelay;
    [SerializeField]
    float chopRidus;

    Renderer renderer;

    public override void StartPattern()
    {
        renderer = enemy.GetComponent<Renderer>();

        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        Vector3 startEnemyPoaition = enemy.transform.position;

        go = Instantiate(bullet, enemy.transform.position, Quaternion.identity, enemy.transform);

        Vector3 player = Vector3.zero;

        for (int i = 0; i < bulletCount; i++)
        {
            float tempTime = rushDelay;
            go.SetActive(true);
            while (tempTime > 0)
            {
                player = PlayerContoller.instance.transform.position;
                go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(go.transform.position.y - player.y, go.transform.position.x - player.x) * Mathf.Rad2Deg));
                tempTime -= Time.deltaTime;

                yield return null;
            }

            go.SetActive(false);
            Vector3 rushRotate = new Vector3(player.x - enemy.transform.position.x, player.y - enemy.transform.position.y, 0).normalized;

            while (IsOutFild(rushRotate))
            {
                enemy.transform.Translate(rushRotate * Time.deltaTime * rushSpeed);

                yield return null;
            }

        }

        enemy.OnWraning();
        enemy.SetWraningScale(chopRidus + 1);
        enemy.transform.position = new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y, enemy.transform.position.z);
        renderer.enabled = false;

        yield return new WaitForSeconds(chopDelay);

        enemy.OffWraning();
        renderer.enabled = true;

        Destroy(go, 1f);

        yield return new WaitUntil(() => go == null);

        enemy.transform.position = startEnemyPoaition;
        enemy.SetWraningScale(Vector3.one * 1.5f);

        BattleManager.ChangeTrun(Trun.아군);
    }

    bool IsOutFild(Vector3 nomal)
    {
        Vector3 chackVector = enemy.transform.position + (nomal * rushSpeed * Time.deltaTime);
        Vector2 fildCenter = BattleManager.battlemanager.Center;
        Vector2 fildRadius = BattleManager.battlemanager.Radius;

        Vector3 enemyToCenterVector = new Vector3(fildCenter.x - enemy.transform.position.x, fildCenter.y - enemy.transform.position.y, 0).normalized;

        if (fildCenter.x + fildRadius.x < chackVector.x && (nomal.x * enemyToCenterVector.x) > 0)
        {
            return true;
        }

        if (fildCenter.x + fildRadius.x > chackVector.x && fildCenter.x - fildRadius.x <chackVector.x && fildCenter.y + fildRadius.y > chackVector.y && fildCenter.y - fildRadius.y < chackVector.y)
        {
            return true;
        }

        return false;
    }
}
