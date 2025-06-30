using System.Collections;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    [SerializeField]
    float spawnTime;

    private void Start()
    {
        SpawnObj();
    }

    void SpawnObj()
    {
        StartCoroutine(temp());
    }

    IEnumerator temp()
    {
        Instantiate(obj, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnTime);

        SpawnObj();
    }
}
