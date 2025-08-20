using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "RubyDrop", menuName = "Scriptable Objects/RubyDrop")]
public class RubyDrop : PatternBase
{
    [Header("소이탄 설정")]
    [SerializeField]
    float firstAttackSize;
    [SerializeField]
    float firstAttackDelay;
    [SerializeField]
    float boomDamage;

    [Header("내려찍기 설정")]
    [SerializeField]
    float takeDownAttackSize;
    [SerializeField]
    float chaseTime;
    [SerializeField]
    float chaseSpeed;
    [SerializeField]
    float takeDownAttackDelay;
    [SerializeField]
    float nextAttackDelay;
    [SerializeField]
    float takeDownAttackDamage;

    [Header("벽")]
    [SerializeField]
    GameObject wall;
    [SerializeField]
    float dropDamage;
    Transform[] walls = new Transform[4];
    Vector3[] wallsPos = new Vector3[4];
    bool[] isMoveableFields = new bool[4];
    int uxoField;
    RubyDropBullet uxo;
    LineRenderer xLine;
    LineRenderer yLine;

    public override void SetPattern()
    {
        base.SetPattern();
        for (int i = 0; i < isMoveableFields.Length; i++)
        {
            isMoveableFields[i] = true;
        }
        wallsPos[0] = new Vector3(center.x + ((radius.x / 2) + 0.25f), center.y + ((radius.y / 2) + 0.25f), 1);
        wallsPos[1] = new Vector3(center.x - ((radius.x / 2) + 0.25f), center.y + ((radius.y / 2) + 0.25f), 1);
        wallsPos[2] = new Vector3(center.x - ((radius.x / 2) + 0.25f), center.y - ((radius.y / 2) + 0.25f), 1);
        wallsPos[3] = new Vector3(center.x + ((radius.x / 2) + 0.25f), center.y - ((radius.y / 2) + 0.25f), 1);
    }

    public override void StartPattern()
    {
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        enemy.animator.SetTrigger("Jump");
        yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("아마튜어_Ruby_Jump"));
        yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);

        //enemy.OffRenderer();
        enemy.OnWraning();
        enemy.SetWraningScale(firstAttackSize);
        enemy.transform.position = new Vector3(center.x, center.y, enemy.transform.position.z);
        enemy.SetAttack(boomDamage, enemyAttackTime);
        
        yield return new WaitForSeconds(firstAttackDelay);

        //enemy.OnRenderer();
        enemy.animator.SetTrigger("Land");
        enemy.OffWraning();
        yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("아마튜어_Ruby_Land"));
        yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 5f / 19f);

        SetUXOField();
        enemy.OnAttack();

        yield return null;

        enemy.SetWraningScale(1);
        SpawnLine();

        for (int i = 0; i < bulletCount; i++)
        {
            go = Instantiate(bullet, enemy.transform.position, Quaternion.identity);
            go.GetComponent<BulletBase>().Setup(this);
            go.GetComponent<RubyDropBullet>().Setup(i);
            if (uxoField == i)
            {
                uxo = go.GetComponent<RubyDropBullet>();
                uxo.SetUXO();
            }
        }

        yield return new WaitForSeconds(bulletSpawnDelay);

        enemy.SetWraningScale(takeDownAttackSize);

        while (true)
        {
            enemy.animator.SetTrigger("Jump");

            yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("아마튜어_Ruby_Jump"));
            yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);

            //enemy.OffRenderer();
            enemy.OnWraning();
            float tempTime = Time.time;
            while (Time.time - tempTime < chaseTime)
            {
                Vector3 temp = PlayerContoller.instance.transform.position - enemy.transform.position;
                enemy.transform.position += temp.normalized * Time.deltaTime * chaseSpeed;
                yield return null;
            }
            yield return new WaitForSeconds(takeDownAttackDelay);

            //enemy.OnRenderer();
            enemy.animator.SetTrigger("Land");
            enemy.OffWraning();

            yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("아마튜어_Ruby_Land"));
            yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 5f / 19f);

            enemy.OnAttack();

            int tempField = -1;
            if (enemy.transform.position.x < center.x)
            {
                tempField = enemy.transform.position.y < center.y ? 2 : 1;
            }
            else
            {
                tempField = enemy.transform.position.y < center.y ? 3 : 0;
            }

            if (tempField == uxoField)
            {
                //끝
                uxo.Boom();
                yield return new WaitForSeconds(1);
                enemy.SetWraningScale(1.5f);
                StopPattern();
                yield break;
            }
            else
            {
                isMoveableFields[tempField] = false;
                walls[tempField] = Instantiate(wall, wallsPos[tempField], Quaternion.identity).transform;
                walls[tempField].transform.localScale = (Vector3)radius + Vector3.one * 0.5f;
            }

            Vector2 chackPlayerPos = (Vector2)PlayerContoller.instance.transform.position - center;
            chackPlayerPos = chackPlayerPos.normalized;
            int playerPos = -1;
            if (chackPlayerPos.x > 0)
            {
                playerPos = chackPlayerPos.y > 0 ? 0 : 3;
            }
            else
            {
                playerPos = chackPlayerPos.y > 0 ? 1 : 2;
            }

            if (playerPos == tempField)
            {
                for (int i = 0; i < isMoveableFields.Length; i++)
                {
                    if (isMoveableFields[++playerPos % 4])
                    {
                        PlayerContoller.instance.transform.position = wallsPos[playerPos % 4];
                        PlayerContoller.instance.GetComponent<IHealthReporter>().TakeDamage(dropDamage);
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(nextAttackDelay);
        }
    }

    void SetUXOField()
    {
        uxoField = Random.Range(0, 4);
    }

    public override void StopPattern()
    {
        base.StopPattern();
        Destroy(uxo.gameObject);
        Destroy(xLine.gameObject);
        Destroy(yLine.gameObject);
        for (int i = 0; i < walls.Length; i++)
        {
            if (!isMoveableFields[i])
            {
                Destroy(walls[i]?.gameObject);
            }
        }
    }

    void SpawnLine()
    {
        xLine = new GameObject().AddComponent<LineRenderer>();
        yLine = new GameObject().AddComponent<LineRenderer>();
        xLine.positionCount = 2;
        xLine.SetPosition(0, new Vector3(center.x - radius.x - 0.5f, center.y, 1));
        xLine.SetPosition(1, new Vector3(center.x + radius.x + 0.5f, center.y, 1));
        xLine.startColor = Color.green;
        xLine.endColor = Color.green;
        xLine.startWidth = 0.1f;
        xLine.endWidth = 0.1f;
        xLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        yLine.positionCount = 2;
        yLine.SetPosition(0, new Vector3(center.x, center.y + radius.y + 0.5f, 1));
        yLine.SetPosition(1, new Vector3(center.x, center.y - radius.y - 0.5f, 1));
        yLine.startColor = Color.green;
        yLine.endColor = Color.green;
        yLine.startWidth = 0.1f;
        yLine.endWidth = 0.1f;
        yLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
    }
}
