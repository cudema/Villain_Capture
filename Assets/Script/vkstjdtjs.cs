using UnityEngine;

public class vkstjdtjs : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindNode();
        }
    }

    void FindNode()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(0, 0.5f, 0), new Vector3(1.5f, 1, 1.5f));

        if (colliders.Length <= 0)
        {
            return;
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
        Debug.Log(sortDistance);
    }

    float GetNodeDistance(Collider node)
    {
        return Vector3.Distance(transform.position, node.transform.position);
    }
}
