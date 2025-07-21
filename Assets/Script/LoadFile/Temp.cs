using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField]
    PrintDialogue printer;

    private void Awake()
    {
        TempTextLoad.SetupText();
    }

    void Start()
    {
        printer.Print(TempTextLoad.GetEnemyDialogue("ITV_001"));
    }
}
