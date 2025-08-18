using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DaggerDrop", menuName = "Scriptable Objects/DaggerDrop")]
public class DaggerDrop : PatternBase
{
    [Header("발판")]
    [SerializeField]
    GameObject floor;
    [SerializeField]
    float xPos;
    [SerializeField]
    float yPos;

    [Header("종료 딜레이")]
    [SerializeField]
    float endDelay;

    GameObject[] floors = new GameObject[4];
    Vector3[] spawnPos = new Vector3[6];

    public override void SetPattern()
    {
        base.SetPattern();

        Vector2 fildCenter = BattleManager.battlemanager.Center;
        Vector2 fildRadius = BattleManager.battlemanager.Radius;

        floors[0] = Instantiate(floor, new Vector3(fildCenter.x - (fildRadius.x - xPos), fildCenter.y - (fildRadius.y - yPos), enemy.transform.position.z), Quaternion.identity);
        floors[1] = Instantiate(floor, new Vector3(fildCenter.x - (fildRadius.x - xPos), fildCenter.y + (fildRadius.y - yPos), enemy.transform.position.z), Quaternion.identity);
        floors[2] = Instantiate(floor, new Vector3(fildCenter.x + (fildRadius.x - xPos), fildCenter.y - (fildRadius.y - yPos), enemy.transform.position.z), Quaternion.identity);
        floors[3] = Instantiate(floor, new Vector3(fildCenter.x + (fildRadius.x - xPos), fildCenter.y + (fildRadius.y - yPos), enemy.transform.position.z), Quaternion.identity);

        spawnPos[0] = new Vector3(fildCenter.x - (fildRadius.x + 0.5f) * (2f / 3f), fildCenter.y + fildRadius.y + 2, enemy.transform.position.z);
        spawnPos[1] = new Vector3(fildCenter.x, fildCenter.y + fildRadius.y + 2, enemy.transform.position.z);
        spawnPos[2] = new Vector3(fildCenter.x + (fildRadius.x + 0.5f) * (2f / 3f), fildCenter.y + fildRadius.y + 2, enemy.transform.position.z);
        spawnPos[3] = new Vector3(fildCenter.x + fildRadius.x + 2, fildCenter.y - (fildRadius.y + 0.5f) * (2f / 3f), enemy.transform.position.z);
        spawnPos[4] = new Vector3(fildCenter.x + fildRadius.x + 2, fildCenter.y, enemy.transform.position.z);
        spawnPos[5] = new Vector3(fildCenter.x + fildRadius.x + 2, fildCenter.y + (fildRadius.y + 0.5f) * (2f / 3f), enemy.transform.position.z);
    }

    public override void StartPattern()
    {
        enemy.StartCoroutine(BingPattern());
    }

    protected override IEnumerator BingPattern()
    {
        if (isEnaged)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                enemy.animator.Play("Attack1");

                int temp1 = Random.Range(0, spawnPos.Length / 2);
                int temp2 = Random.Range(spawnPos.Length / 2, spawnPos.Length);

                go = Instantiate(bullet, spawnPos[temp1], Quaternion.identity, bulletParent);
                go.GetComponent<BulletBase>().Setup(this);
                go = Instantiate(bullet, spawnPos[temp2], Quaternion.identity, bulletParent);
                go.GetComponent<BulletBase>().Setup(this);

                yield return new WaitForSeconds(bulletSpawnDelay);
            }
        }
        else
        {
            for (int i = 0; i < bulletCount; i++)
            {
                enemy.animator.Play("Attack0");
                int temp = Random.Range(0, spawnPos.Length);

                go = Instantiate(bullet, spawnPos[temp], Quaternion.identity, bulletParent);
                go.GetComponent<BulletBase>().Setup(this);

                yield return new WaitForSeconds(bulletSpawnDelay);
            }
        }

        yield return new WaitUntil(() => go == null);
        yield return new WaitForSeconds(endDelay);

        StopPattern();
    }

    public override void StopPattern()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            Destroy(floors[i]);
        }
        base.StopPattern();
    }
}
