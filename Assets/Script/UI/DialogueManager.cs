using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField]
    PrintDialogue enemyPrinter;
    [SerializeField]
    GameObject enemyPrinterPanal;
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
    }

    void OnEnable()
    {
        BattleManager.EndPlayerAction += OffPlayerPrinterPanal;
    }

    void OnDisable()
    {
        BattleManager.EndPlayerAction -= OffPlayerPrinterPanal;
    }

    public void PrintItem(string itemName, string naxtDialogueID)
    {
        string temp = $"당신은 {itemName}을(를) 사용했다.";
        playerPrinterPanal.SetActive(true);
        StartCoroutine(playerPrinter.PrintItemCoroutine(temp, naxtDialogueID));
    }

    public void PrintItem(string itemName, float heal, float healPercent)
    {
        string temp = $"당신은 {itemName}을(를) 사용했다.\n체력 {heal + (PlayerContoller.instance.health.GetMaxHealth() * healPercent)}을 회복했다.";
        playerPrinterPanal.SetActive(true);
        StartCoroutine(playerPrinter.PrintItemCoroutine(temp));
    }

    public IEnumerator PrintDialogue(string interviewID)
    {
        if (interviewID == "END")
        {
            Debug.Log("대사 끝");
            BattleManager.battlemanager.StopAction();
            yield break;
        }
        if (interviewID == "")
        {
            Debug.LogWarning("대사 없음");
            yield break;
        }
        EnemyDialogue temp;
        Emotion enemyemotion = BattleManager.battlemanager.CurrentEnemy.GetEnemyEmotion();
        temp = TempTextLoad.GetEnemyDialogue(interviewID, enemyemotion);
        if (temp.emotionalGauge != null)
        {
            BattleManager.battlemanager.CurrentEnemy.EmotionalGauge += (int)temp.emotionalGauge;
            Debug.Log(temp.emotionalGauge);
            if (enemyemotion != BattleManager.battlemanager.CurrentEnemy.GetEnemyEmotion())
            {
                temp = TempTextLoad.GetEnemyDialogue(interviewID, BattleManager.battlemanager.CurrentEnemy.GetEnemyEmotion());
            }
        }

        if (temp.speaker == "Player")
        {
            playerPrinterPanal.SetActive(true);
            yield return StartCoroutine(playerPrinter.PrintTextCoroutine(temp));
        }
        else
        {
            enemyPrinterPanal.SetActive(true);
            yield return StartCoroutine(enemyPrinter.PrintTextCoroutine(temp));
        }
    }

    void OffPlayerPrinterPanal()
    {
        playerPrinterPanal.SetActive(false);
        enemyPrinterPanal.SetActive(false);
    }
}
