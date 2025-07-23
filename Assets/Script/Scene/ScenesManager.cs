using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance { private set; get; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadBattleScene(GameObject enemy)
    {
        StartCoroutine(Load("Battle", enemy));
    }

    IEnumerator Load(string SceneName, GameObject enemy)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        //asyncOperation.allowSceneActivation = false;
        Debug.Log(0);
        yield return asyncOperation;
        Debug.Log(1);
        BattleManager.battlemanager.SetEnemy(Instantiate(enemy).GetComponent<EnemyBase>());
        //asyncOperation.allowSceneActivation = true;
    }
}
