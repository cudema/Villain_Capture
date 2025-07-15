using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DaggerRain", menuName = "Scriptable Objects/DaggerRain")]
public class DaggerRain : PatternBase
{
    [Header("쏟아지는 단검")]
    [SerializeField]
    float durationTime;
    [SerializeField]
    float spawnRadius;
    [SerializeField]
    float donSpawnRadius;

    public override void StartPattern()
    {
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        float time = durationTime;

        float minSpawnX = BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x;
        float maxSpawnX = BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x;

        float spawnY = BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y + 2;

        int temp = 0;

        while (time > 0)
        {
            if (temp++ % 3 == 0)
            {
                go = Instantiate(bullet, new Vector3(PlayerContoller.instance.transform.position.x, spawnY, enemy.transform.position.z), Quaternion.identity, bulletParent);
                go.GetComponent<BulletBase>().Setup(this);
            }
            else
            {
                float spawnX = GetRandomXPos(minSpawnX, maxSpawnX);

                go = Instantiate(bullet, new Vector3(spawnX, spawnY, enemy.transform.position.z), Quaternion.identity, bulletParent);
                go.GetComponent<BulletBase>().Setup(this);
            }

            time -= bulletSpawnDelay;
            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        StopPattern();
    }

    float GetRandomXPos(float min, float max)
    {
        float temp;

        float playerX = PlayerContoller.instance.transform.position.x;

        do
        {
            temp = Random.Range(min, max);
        } while (((temp < playerX - spawnRadius || temp > playerX - donSpawnRadius) && temp < playerX) || ((temp > playerX + spawnRadius || temp < playerX + donSpawnRadius) && temp > playerX));

        return temp;
    }
}
