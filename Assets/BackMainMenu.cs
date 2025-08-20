using UnityEngine;

public class BackMainMenu : MonoBehaviour
{
    public void BackMenu()
    {
        ScenesManager.instance.LoadTempMain();
    }
}
