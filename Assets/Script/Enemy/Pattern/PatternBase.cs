using System.Collections;
using UnityEngine;

[System.Serializable]
public struct BulletData
{
    public string name;
    public float damage;
    public float speed;
}

public class PatternBase : ScriptableObject
{
    [Header("�ʵ� ����")]
    [SerializeField]
    protected Vector2 center;
    [SerializeField]
    protected Vector2 radius;
    [SerializeField]
    protected bool isChangedFild = false;

    [Header("���� ��� ����")]
    [SerializeField]
    protected PlayMode state;

    [Header("���� ź ����")]
    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    public BulletData[] bulletDatas;
    [SerializeField]
    protected int bulletCount;
    [SerializeField]
    protected float bulletSpawnDelay;
    [SerializeField]
    public float attackDelay;

    [Header("����ȭ ����")]
    [SerializeField]
    public bool isEnaged;

    protected EnemyBase enemy;
    protected GameObject go;
    protected Transform bulletParent;

    public void Setup(EnemyBase enemy)
    {
        this.enemy = enemy;
        bulletParent = enemy.transform.GetChild(1);
    }

    public virtual void StartPattern()
    {
        if (isChangedFild)
        {
            BattleManager.battlemanager.ChangeFild(center, radius);
        }

        PlayerContoller.instance.ChangePlayMode(state);

        enemy.StartCoroutine(BingPattern());
    }

    protected virtual IEnumerator BingPattern()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            go = Instantiate(bullet, enemy.transform.position, Quaternion.identity, bulletParent);
            go.GetComponent<BulletBase>().Setup(this);
            Destroy(go, 3.0f);

            yield return new WaitForSeconds(bulletSpawnDelay);
        }

        yield return new WaitUntil(() => go == null);

        BattleManager.battlemanager.ChangeTrun(Trun.아군);
    }

    public virtual void StopPattern()
    {
        enemy.StopAllCoroutines();
        Destroy(go);
        for (int i = 0; i < bulletParent.childCount; i++)
        {
            Destroy(bulletParent.GetChild(i).gameObject);
        }

        BattleManager.battlemanager.ChangeTrun(Trun.아군);
    }
}
