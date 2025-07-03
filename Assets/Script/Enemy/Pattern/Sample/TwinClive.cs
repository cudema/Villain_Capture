using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TwinClive", menuName = "Scriptable Objects/TwinClive")]
public class TwinClive : PatternBase
{
    [SerializeField, Range(0, 1)]
    float radiusRange;
    [SerializeField]
    float tiltRange;

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

            GameObject cloen = Instantiate(bullet, new Vector3(ranX, ranY, enemy.transform.position.z), Quaternion.Euler(new Vector3(0, 0, tilt)));
            cloen.GetComponent<BulletBase>().Setup(bulletSpeed);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitForSeconds(bulletSpeed);

        BattleManager.ChangeTrun(Trun.¾Æ±º);
    }
}
