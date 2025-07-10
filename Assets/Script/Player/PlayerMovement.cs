using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    Vector3 moveDirection;

    public void ToMove()
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

    public void ReturnPosition()
    {
        transform.position = new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y, transform.position.z);
    }

    public void SetDirection(Vector2 vector)
    {
        moveDirection = new Vector3(vector.x, vector.y, 0);
    }
}
