using System.Collections;
using Unity.Mathematics;
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

    //임시조치
    Renderer playerRenderer;

    void Awake()
    {
        //임시조치
        playerRenderer = GetComponent<Renderer>();
    }

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

        Collider[] collider = Physics.OverlapBox(cloen, new Vector3(0.5f, 0.5f, 1));
        if (collider.Length > 0)
        {
            for (int i = 0; i < collider.Length; i++)
            {
                if (collider[i].CompareTag("Wall"))
                {
                    Vector3 temp = collider[i].transform.position - transform.position;
                    temp = temp.normalized;
                    if (Mathf.Abs(temp.x) > 0.707f)
                    {
                        cloen.x = transform.position.x;
                    }
                    if (Mathf.Abs(temp.y) > 0.707f)
                    {
                        cloen.y = transform.position.y;
                    }
                }
            }
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
        StartCoroutine(PlayerStartPositionAnimation());
        StartCoroutine(GoToStartPosition(new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y, transform.position.z)));
    }

    IEnumerator PlayerStartPositionAnimation()
    {
        for (int i = 0; i < 5; i++)
        {
            playerRenderer.enabled = i % 2 == 0 ? false : true;

            yield return new WaitForSeconds(0.15f);
        }
        playerRenderer.enabled = true;
        BattleManager.battlemanager.isOnEnemy = true;
        yield break;
    }
    IEnumerator GoToStartPosition(Vector3 pos)
    {
        Vector3 temp = pos - transform.position;
        while (!BattleManager.battlemanager.isOnEnemy)
        {
            transform.position += temp * Time.deltaTime / (0.15f * 5.5f);
            yield return null;
        }
        transform.position = pos;
    }

    public void StartMovePosition()
    {
        switch (PlayerContoller.instance.GetPlayMode())
        {
            case PlayMode.일반:
                StartCoroutine(PlayerStartPositionAnimation());
                StartCoroutine(GoToStartPosition(new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y, transform.position.z)));
                break;
            case PlayMode.플렛포머:
                v = 0;
                isjumpable = false;
                StartCoroutine(PlayerStartPositionAnimation());
                StartCoroutine(GoToStartPosition(new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y, transform.position.z)));
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
