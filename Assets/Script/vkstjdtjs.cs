using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class vkstjdtjs : MonoBehaviour
{
    public bool isHaveJudgment = false;

    public IEnumerator HitNode(int count)
    {
        InputManager.inputManager.photo.performed += OnAttackjudgment;
        float judTemp = 0;

        for (int i = 0; i < count; i++)
        {
            isHaveJudgment = false;
            yield return new WaitUntil(() => isHaveJudgment);

            float temp = FindNode();

            if (temp >= 0.9f)
            {
                judTemp += 0 / count;
                continue;
            }
            if (temp >= 0.5f)
            {
                judTemp += 0.7f / count;
                continue;
            }
            if (temp >= 0)
            {
                judTemp += 1.0f / count;
                continue;
            }
        }
        PlayerContoller.instance.SetCurrentAttackJudgment(judTemp);
        InputManager.inputManager.photo.performed -= OnAttackjudgment;
    }

    float FindNode()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(0, 4, 0), new Vector3(1.5f, 12, 1.5f));

        if (colliders.Length <= 0)
        {
            Debug.Log("판정 가능한 노드 없음");
            return -1;
        }

        Collider sortCollider = colliders[0];
        float sortDistance = GetNodeDistance(colliders[0]);

        for (int i = 1; i < colliders.Length; i++)
        {
            if (sortDistance > GetNodeDistance(colliders[i]))
            {
                sortDistance = GetNodeDistance(colliders[i]);
                sortCollider = colliders[i];
            }
        }

        Destroy(sortCollider.gameObject);
        return sortDistance;
    }

    float GetNodeDistance(Collider node)
    {
        return Vector3.Distance(transform.position, node.transform.position);
    }

    void OnAttackjudgment(InputAction.CallbackContext context)
    {
        isHaveJudgment = true;
    }
}
