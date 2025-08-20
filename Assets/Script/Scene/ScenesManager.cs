using System;
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

    public void LoadTempMain()
    {
        StartCoroutine(Load("tempStartMenu"));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator Load(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        //asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
    }

    public void LoadBattleScene(GameObject enemy)
    {
        StartCoroutine(LoadBattle("Battle", enemy));
    }

    IEnumerator LoadBattle(string SceneName, GameObject enemy)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        //asyncOperation.allowSceneActivation = false;
        Debug.Log(0);
        yield return asyncOperation;
        Debug.Log(1);
        BattleManager.battlemanager.SetEnemy(Instantiate(enemy).GetComponent<EnemyBase>());
        //asyncOperation.allowSceneActivation = true;
    }

    public void LoadWin()
    {
        StartCoroutine(Load("GameClaer"));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadLose()
    {
        StartCoroutine(Load("GameOver"));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMainMenu()
    {
        StartCoroutine(Load("MainMenu"));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
