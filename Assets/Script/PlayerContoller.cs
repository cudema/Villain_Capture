using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField]
    float speed;

    [Header("이동 반경 설정")]
    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 radius;

    [Header("촬영")]
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

        if (cloen.x < center.x - radius.x || cloen.x > center.x + radius.x)
        {
            cloen.x = transform.position.x;
        }

        if (cloen.y < center.y - radius.y || cloen.y > center.y + radius.y)
        {
            cloen.y = transform.position.y;
        }


        transform.position = cloen;

    }

    void ReturnPosition()
    {
        transform.position = new Vector3(center.x, center.y, transform.position.z);
    }

    public void SetDirection(Vector2 vector)
    {
        moveDirection = new Vector3(vector.x, vector.y, 0);
    }

    public Vector3[] GetMoveRadius()
    {
        Vector3[] vectors = new Vector3[5];
        vectors[0] = new Vector3(center.x - radius.x - 0.5f, center.y + radius.y + 0.5f, 0);
        vectors[1] = new Vector3(center.x + radius.x + 0.5f, center.y + radius.y + 0.5f, 0);
        vectors[2] = new Vector3(center.x + radius.x + 0.5f, center.y - radius.y - 0.5f, 0);
        vectors[3] = new Vector3(center.x - radius.x - 0.5f, center.y - radius.y - 0.5f, 0);
        vectors[4] = new Vector3(center.x - radius.x - 0.5f, center.y + radius.y + 0.5f, 0);

        return vectors;
    }

    public void SetPattern()
    {
        BattleManager.PatternStart(pattern);
    }
}
