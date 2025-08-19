using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SoulRelease", menuName = "Scriptable Objects/SoulRelease")]
public class SoulRelease : PatternBase
{
    [Header("����")]
    [SerializeField]
    float rushDelay;
    [SerializeField]
    float rushSpeed;
    [SerializeField]
    float attackRadius;

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
        int count = 3;

        for (int i = 0; i < bulletCount; i++)
        {
            float rotate = -15;

            enemy.animator.Play("Attack2");

            for (int j = 0; j < count; j++)
            {
                go = Instantiate(bullet, enemy.transform.position, Quaternion.Euler(new Vector3(0, 0, rotate)));
                go.GetComponent<BulletBase>().Setup(this);

                rotate += 30 / (count - 1);
            }

            if (count == 3)
            {
                count++;
            }
            else
            {
                count--;
            }

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitForSeconds(rushDelay);

        Vector3 player = PlayerContoller.instance.transform.position;

        enemy.animator.SetBool("IsRush", true);
        enemy.SetWraningScale(1f);
        enemy.OnAttack();

        while (Vector3.Distance(player, enemy.transform.position) > 0.1f)
        {
            enemy.transform.position += (player - enemy.transform.position).normalized * rushSpeed * Time.deltaTime;

            yield return null;
        }

        enemy.animator.SetBool("IsRush", false);
        enemy.SetWraningScale(attackRadius * 2);
        enemy.OnWraning();
        if (!isEnaged)
        {
            enemy.OnParringable();
        }

        yield return new WaitForSeconds(attackDelay);

        enemy.OffParringable();

        enemy.animator.Play("Attack1");
        yield return null;
        yield return new WaitUntil(() => enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 18f / 24f);

        enemy.OffWraning();
        enemy.OnAttack();

        yield return new WaitForSeconds(attackDelay);

        enemy.SetWraningScale(1.5f);

        StopPattern();
    }
}
