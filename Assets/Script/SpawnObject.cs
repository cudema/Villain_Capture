using System.Collections;
using UnityEngine;

[System.Serializable]
public class NodePattern
{
    public GameObject node;
    public int spawnNodeCount;
    public float spawnTime;
    public float patternEndTime;
}

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    NodePattern samplePattern;

    private void Start()
    {

    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        BattleManager.battlemanager.StopAction();
    }

    public void SpawnObj(NodePattern pattern)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowNode(pattern));
    }

    IEnumerator ShowNode(NodePattern pattern)
    {
        GameObject[] temp = new GameObject[pattern.spawnNodeCount];

        for (int i = 0; i < pattern.spawnNodeCount; i++)
        {
            yield return new WaitForSeconds(pattern.spawnTime);

            temp[i] = Instantiate(pattern.node, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        }

        yield return StartCoroutine(GetComponent<vkstjdtjs>().HitNode(pattern.spawnNodeCount));

        //PlayerContoller.instance.GetComponent<PlayerContoller>().Attack();

        yield return new WaitForSeconds(pattern.patternEndTime);

        gameObject.SetActive(false);
    }
}
