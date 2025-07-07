using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BladeWave", menuName = "Scriptable Objects/BladeWave")]
public class BladeWave : PatternBase
{
    protected override IEnumerator BingPattern()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            go = Instantiate(bullet, enemy.transform.position, Quaternion.identity);
            go.GetComponent<BulletBase>().Setup(this);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitUntil(() => go == null);

        BattleManager.ChangeTrun(Trun.¾Æ±º);
    }
}
