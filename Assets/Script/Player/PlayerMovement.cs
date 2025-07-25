using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpSpeed;

    bool isjumpable = false;
    Vector3 moveDirection;

    float v;

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

    public void ToJumpMove()
    {
        Vector3 cloen = transform.position + (moveDirection * speed * Time.deltaTime);

        if (cloen.x < BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x || cloen.x > BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x)
        {
            cloen.x = transform.position.x;
        }

        if (moveDirection.y > 0.5f && isjumpable)
        {
            v = jumpSpeed;
            isjumpable = false;
        }

        cloen.y = transform.position.y + (v * Time.deltaTime) + (0.5f * -9.8f * Time.deltaTime * Time.deltaTime);
        v += -9.8f * Time.deltaTime;

        if (cloen.y - transform.position.y > 0 && moveDirection.y < 0.5f)
        {
            v /= 2;
        }

        if (cloen.y - transform.position.y < 0)
        {
            Collider[] collider = Physics.OverlapBox(transform.position - new Vector3(0, 0.5f, 0), new Vector3(0.5f, 0.0001f, 1));
            
            if (collider.Length > 0 && collider[0].CompareTag("Floor"))
            {
                isjumpable = true;
                v = 0;
                cloen.y = transform.position.y;
            }
        }

        if (cloen.y > BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y)
        {
            v = 0;
            cloen.y = transform.position.y;
        }

        if (cloen.y < BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y)
        {
            isjumpable = true;
            v = 0;
            cloen.y = transform.position.y;
        }

        transform.position = cloen;
    }

    public void ReturnPosition()
    {
        v = 0;
        isjumpable = false;
        transform.position = new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y, transform.position.z);
    }

    public void StartMovePosition()
    {
        switch (PlayerContoller.instance.GetPlayMode())
        {
            case PlayMode.일반:
                transform.position = new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y, transform.position.z);
                break;
            case PlayMode.플렛포머:
                v = 0;
                isjumpable = false;
                transform.position = new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y, transform.position.z);
                break;
            default:
                break;
        }
    }

    public void SetDirection(Vector2 vector)
    {
        moveDirection = new Vector3(vector.x, vector.y, 0);
    }
}
