using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundZero", menuName = "Scriptable Objects/GroundZero")]
public class GroundZero : PatternBase
{
    [SerializeField]
    float startSootDelay;
    [SerializeField]
    float scalePerSecond;
    public float ScalePerSecond
    {
        get => scalePerSecond; private set => scalePerSecond = value;
    }

    [Header("돌진 설정")]
    [SerializeField]
    float rushDelay;
    [SerializeField]
    float collisionDistance;
    [SerializeField, Range(0, 1)]
    float radiusRange;
    [SerializeField]
    float patternEndDelay;

    Vector3 startPosVector;

    public override void SetPattern()
    {
        base.SetPattern();
    }

    public override void StartPattern()
    {
        startPosVector = enemy.transform.position;
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        enemy.OffRenderer();

        Vector2 tempVector = BattleManager.battlemanager.Radius * radiusRange;
        Vector2 center = BattleManager.battlemanager.Center;
        float ranX = Random.Range(center.x - tempVector.x, center.x + tempVector.x);
        float ranY = Random.Range(center.y - tempVector.y, center.y + tempVector.y);

        enemy.transform.position = new Vector3(ranX, ranY, enemy.transform.position.z);
        enemy.OnWraning();

        yield return new WaitForSeconds(rushDelay);

        enemy.OffWraning();
        enemy.OnRenderer();
        enemy.OnParringable();

        yield return new WaitForSeconds(startSootDelay);

        enemy.OffParringable();

        for (int i = 0; i < bulletCount; i++)
        {
            go = Instantiate(bullet, enemy.transform.position, Quaternion.identity, bulletParent);
            go.GetComponent<BulletBase>().Setup(this);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitUntil(() => go == null);

        enemy.transform.position = startPosVector;

        yield return new WaitForSeconds(patternEndDelay);

        BattleManager.battlemanager.ChangeTrun(Trun.아군);
    }
}
