using System.Collections;
using UnityEngine;

[System.Serializable]
public class PatternBase : ScriptableObject
{
    [Header("패턴 탄 설정")]
    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected int bulletCount;
    [SerializeField]
    protected float bulletSpawnDelay;

    protected MonoBehaviour enemy;

    public void Setup(MonoBehaviour enemy)
    {
        this.enemy = enemy;
    }

    public void StartPattern()
    {
        enemy.StartCoroutine(BingPattern());
    }

    protected virtual IEnumerator BingPattern()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject go = Instantiate(bullet, enemy.transform.position, Quaternion.identity);
            go.GetComponent<SampleBullet>().Setup(bulletSpeed);
            Destroy(go, 3.0f);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitForSeconds(3.0f);

        BattleManager.ChangeTrun(Trun.아군);
    }
}
