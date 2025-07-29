using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BladeWave", menuName = "Scriptable Objects/BladeWave")]
public class BladeWave : PatternBase
{
    public override void SetPattern()
    {
        base.SetPattern();
    }

    public override void StartPattern()
    {
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            go = Instantiate(bullet, enemy.transform.position, Quaternion.identity, bulletParent);
            go.GetComponent<BulletBase>().Setup(this);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitUntil(() => go == null);

        BattleManager.battlemanager.ChangeTrun(Trun.아군);
    }
}
