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
        BattleManager.PlayerAction(BattleAction.√ ±‚»≠);
    }

    public void SpawnObj(NodePattern pattern)
    {
        gameObject.SetActive(true);
        StartCoroutine(temp(pattern));
    }

    IEnumerator temp(NodePattern pattern)
    {
        for (int i = 0; i < pattern.spawnNodeCount; i++)
        {
            Instantiate(pattern.node, transform.position + new Vector3(0, 10, 0), Quaternion.identity);

            yield return new WaitForSeconds(pattern.spawnTime);
        }

        yield return new WaitForSeconds(pattern.patternEndTime);

        gameObject.SetActive(false);
    }
}
