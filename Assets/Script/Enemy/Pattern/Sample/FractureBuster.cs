using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "FractureBuster", menuName = "Scriptable Objects/FractureBuster")]
public class FractureBuster : PatternBase
{
    [SerializeField]
    float smallBulletArrivalTime;
    public float SmallBulletArrivalTime
    {
        get => smallBulletArrivalTime; private set => smallBulletArrivalTime = value;
    }

    Vector2[][] randomPos = new Vector2[3][];
    [Header("돌덩이 간격")]
    [SerializeField]
    float minDistance;

    Vector2[] ativePos = new Vector2[] { Vector2.zero, Vector2.zero, Vector2.zero };

    Vector3 enemyPos;
    public Vector2[] AtivePos
    {
        get => ativePos; private set => ativePos = value;
    }

    [Header("돌진 설정")]
    [SerializeField]
    float rushDelay;
    [SerializeField]
    float rushSpeed;
    [SerializeField]
    float collisionDistance;
    [SerializeField]
    float sternTime;

    GameObject warning;

    public override void SetPattern()
    {
        base.SetPattern();

        randomPos[0] = new Vector2[2];
        randomPos[1] = new Vector2[2];
        randomPos[2] = new Vector2[2];
        randomPos[0][0] = new Vector2(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y);
        randomPos[0][1] = new Vector2(BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x, BattleManager.battlemanager.Center.y + (BattleManager.battlemanager.Radius.y * 0.3333f));
        randomPos[1][0] = randomPos[0][0] - new Vector2(0, BattleManager.battlemanager.Radius.y * 0.6666f);
        randomPos[1][1] = randomPos[0][1] - new Vector2(0, BattleManager.battlemanager.Radius.y * 0.6666f);
        randomPos[2][0] = randomPos[1][0] - new Vector2(0, BattleManager.battlemanager.Radius.y * 0.6666f);
        randomPos[2][1] = randomPos[1][1] - new Vector2(0, BattleManager.battlemanager.Radius.y * 0.6666f);
    }

    public override void StartPattern()
    {
        enemyPos = enemy.transform.position;
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            ativePos[0] = Vector2.zero;
            ativePos[1] = Vector2.zero;
            ativePos[2] = Vector2.zero;

            SetRandomPos(randomPos[1], 1);
            SetRandomPos(randomPos[0], 0);
            SetRandomPos(randomPos[2], 2);

            go = Instantiate(bullet, enemy.transform.position, Quaternion.identity);
            go.GetComponent<FractureBusterBullet>().Setup(this);
            warning = go.transform.GetChild(0).gameObject;

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitForSeconds(attackDelay - (18f / 24f));

        enemy.animator.SetTrigger("FB");

        //yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 18 / 24);

        yield return new WaitUntil(() => go.GetComponent<FractureBusterBullet>().Arrival());

        Vector3 player = Vector3.zero;

        warning.SetActive(true);
        float tempTime = rushDelay;

        while (tempTime > 0)
        {
            player = PlayerContoller.instance.transform.position;

            warning.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(warning.transform.position.y - player.y, warning.transform.position.x - player.x) * Mathf.Rad2Deg));
            tempTime -= Time.deltaTime;
            yield return null;
        }


        warning.SetActive(false);
        enemy.OnAttack();
        enemy.animator.SetBool("IsRun", true);
        while (enemy.transform.position.x > -10)
        {
            enemy.transform.Translate((player - enemyPos).normalized * rushSpeed * Time.deltaTime);

            if (Vector2.Distance(ativePos[1], (Vector2)enemy.transform.position) < collisionDistance)
            {
                enemy.animator.SetBool("IsRun", false);
                Destroy(go);
                yield return new WaitForSeconds(sternTime);
                StopPattern();
                yield break;
            }

            yield return null;
        }

        enemy.animator.SetBool("IsRun", false);
        StopPattern();
    }

    void SetRandomPos(Vector2[] pos, int index)
    {
        Vector2 temp;
        temp.x = Random.Range(pos[0].x, pos[1].x);
        temp.y = Random.Range(pos[0].y, pos[1].y);

        foreach (Vector2 vec in ativePos)
        {
            if (Vector2.Distance(vec, temp) < minDistance)
            {
                SetRandomPos(pos, index);
                return;
            }
        }

        ativePos[index] = temp;
    }
}
