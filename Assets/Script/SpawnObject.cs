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
        BattleManager.PlayerAction((int)BattleAction.√ ±‚»≠);
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
            temp[i] = Instantiate(pattern.node, transform.position + new Vector3(0, 10, 0), Quaternion.identity);

            yield return new WaitForSeconds(pattern.spawnTime);
        }

        yield return new WaitUntil(() => temp[pattern.spawnNodeCount - 1] == null);
        yield return new WaitForSeconds(0.2f);

        gameObject.SetActive(false);
    }
}
