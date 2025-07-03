using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField]
    float speed;

    [Header("ÃÔ¿µ")]
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

    public Vector3[] GetMoveRadius()
    {
        Vector3[] vectors = new Vector3[5];
        vectors[0] = new Vector3(BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x - 0.5f, BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y + 0.5f, 0);
        vectors[1] = new Vector3(BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x + 0.5f, BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y + 0.5f, 0);
        vectors[2] = new Vector3(BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x + 0.5f, BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y - 0.5f, 0);
        vectors[3] = new Vector3(BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x - 0.5f, BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y - 0.5f, 0);
        vectors[4] = new Vector3(BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x - 0.5f, BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y + 0.5f, 0);

        return vectors;
    }

    public void SetPattern()
    {
        BattleManager.PatternStart(pattern);
    }
}
