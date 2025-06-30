using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    private void Update()
    {
        transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
    }
}
