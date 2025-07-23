using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TwinClive", menuName = "Scriptable Objects/TwinClive")]
public class TwinClive : PatternBase
{
    [SerializeField, Range(0, 1)]
    float radiusRange;
    [SerializeField]
    float tiltRange;
    [SerializeField]
    bool randomPosition;

    protected override IEnumerator BingPattern()
    {
        Vector2 tempVector = BattleManager.battlemanager.Radius * radiusRange;
        Vector2 center = BattleManager.battlemanager.Center;
        float ranX = Random.Range(center.x - tempVector.x, center.x + tempVector.x);
        float ranY = Random.Range(center.y - tempVector.y, center.y + tempVector.y);

        float temp = 0;

        for (int i = 0; i < bulletCount; i++)
        {
            float tilt = Random.Range(-tiltRange, tiltRange) + temp;
            temp += 90;

            GameObject cloen = Instantiate(bullet, new Vector3(ranX, ranY, enemy.transform.position.z), Quaternion.Euler(new Vector3(0, 0, tilt)), bulletParent);
            cloen.GetComponent<BulletBase>().Setup(this);

            if (randomPosition)
            {
                ranX = Random.Range(center.x - tempVector.x, center.x + tempVector.x);
                ranY = Random.Range(center.y - tempVector.y, center.y + tempVector.y);
            }

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitForSeconds(attackDelay);

        BattleManager.battlemanager.ChangeTrun(Trun.아군);
    }
}
