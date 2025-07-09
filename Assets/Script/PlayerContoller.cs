using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField]
    float speed;

    [Header("√‘øµ")]
    [SerializeField]
    NodePattern pattern;

    Vector3 moveDirection;

    private void Start()
    {
        BattleManager.EndEnemyTrun += ReturnPosition;
    }

    private void Update()
    {
        ToMove();
    }

    void ToMove()
    {
        Vector3 cloen = transform.position + (moveDirection * speed * Time.deltaTime);

        if (cloen.x < BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x || cloen.x > BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x)
        {
            cloen.x = transform.position.x;
        }

        if (cloen.y < BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y || cloen.y > BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y)
        {
            cloen.y = transform.position.y;
        }


        transform.position = cloen;

    }

    void ReturnPosition()
    {
        transform.position = new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y, transform.position.z);
    }

    public void SetDirection(Vector2 vector)
    {
        moveDirection = new Vector3(vector.x, vector.y, 0);
    }

    public void SetPattern()
    {
        BattleManager.PatternStart(pattern);
    }

    public void Attack()
    {
        BattleManager.CurrentEnemy.TakeDamage(PlayerData.player.Damage);
        PlayerData.player.CurrentAttackJudgment = 0;
    }
}
