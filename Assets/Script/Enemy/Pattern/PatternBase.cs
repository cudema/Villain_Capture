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
    [SerializeField]
    protected float attackDelay;

    protected MonoBehaviour enemy;
    protected GameObject go;

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
            go = Instantiate(bullet, enemy.transform.position, Quaternion.identity);
            go.GetComponent<BulletBase>().Setup(bulletSpeed);
            Destroy(go, 3.0f);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitUntil(() => go == null);

        BattleManager.ChangeTrun(Trun.아군);
    }
}
