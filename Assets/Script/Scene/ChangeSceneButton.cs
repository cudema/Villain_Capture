using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    public void ChangeScene()
    {
        ScenesManager.instance.LoadBattleScene(enemy);
    }
}
