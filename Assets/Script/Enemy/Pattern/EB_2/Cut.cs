using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Cut", menuName = "Scriptable Objects/Cut")]
public class Cut : PatternBase
{
    public override void StartPattern()
    {
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        yield return new WaitForSeconds(attackDelay);

        Vector3 temp = new Vector3(PlayerContoller.instance.transform.position.x, BattleManager.battlemanager.Center.y, enemy.transform.position.z);

        for (int i = 0; i < bulletCount; i++)
        {
            yield return new WaitForSeconds(bulletSpawnDelay);
            go = Instantiate(bullet, temp, Quaternion.identity, bulletParent);
            go.GetComponent<BulletBase>().Setup(this);
            Destroy(go, 1f);
        }

        yield return new WaitUntil(() => go == null);

        BattleManager.battlemanager.ChangeTrun(Trun.아군);
    }
}
