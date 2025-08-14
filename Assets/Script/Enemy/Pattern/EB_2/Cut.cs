using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Cut", menuName = "Scriptable Objects/Cut")]
public class Cut : PatternBase
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
        yield return new WaitForSeconds(attackDelay);

        Vector3 temp = new Vector3(PlayerContoller.instance.transform.position.x, BattleManager.battlemanager.Center.y, enemy.transform.position.z);

        int aniTemp = Random.Range(0, 2);
        enemy.animator.Play($"Attack{aniTemp}");
        yield return null;
        yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);

        for (int i = 0; i < bulletCount; i++)
        {
            yield return new WaitForSeconds(bulletSpawnDelay);
            go = Instantiate(bullet, temp, Quaternion.identity, bulletParent);
            go.GetComponent<BulletBase>().Setup(this);
        }

        yield return new WaitUntil(() => go == null);

        BattleManager.battlemanager.ChangeTrun(Trun.아군);
    }
}
