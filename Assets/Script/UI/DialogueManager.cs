using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField]
    PrintDialogue enemyPrinter;
    [SerializeField]
    PrintDialogue playerPrinter;
    [SerializeField]
    GameObject playerPrinterPanal;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        TempTextLoad.SetupText();
        BattleManager.EndPlayerAction += OffPlayerPrinterPanal;
    }

    public void PrintDialogue(string interviewID)
    {
        if (interviewID == "END")
        {
            Debug.Log("대사 끝");
            BattleManager.battlemanager.StopAction();
            return;
        }
        if (interviewID == "")
        {
            Debug.Log("대사 없음");
            return;
        }
        EnemyDialogue temp;

        temp = TempTextLoad.GetEnemyDialogue(interviewID, BattleManager.CurrentEnemy.GetEnemyEmotion());
        if (temp.emotionalGauge != null)
        {
            BattleManager.CurrentEnemy.EmotionalGauge += (int)temp.emotionalGauge;
        }

        if (temp.speaker == "Player")
            {
                playerPrinterPanal.SetActive(true);
                playerPrinter.Print(temp);
            }
            else
            {
                enemyPrinter.Print(temp);
            }
    }

    void OffPlayerPrinterPanal()
    {
        playerPrinterPanal.SetActive(false);
    }
}
