using UnityEngine;

public class SampleBullet : MonoBehaviour
{
    float speed;

    public void Setup(float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    }
}
