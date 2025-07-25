using UnityEngine;

public class BattleRunConttorl : MonoBehaviour
{
    [SerializeField, Range(0, 1)]
    float nomalProbability;
    [SerializeField, Range(0, 1)]
    float enageProbability;
    [SerializeField, Range(0, 1)]
    float turnProbability;

    [SerializeField]
    int bonusTurnCount;

    public void BattleRunRandom()
    {
        float temp = nomalProbability;
        if (BattleManager.battlemanager.CurrentEnemy.GetIsEnage())
        {
            temp += enageProbability;
        }
        if (BattleManager.battlemanager.turnCount >= bonusTurnCount)
        {
            temp += turnProbability;
        }

        float tempRandom = Random.Range(0.0f, 1.0f);

        if (temp > tempRandom)
        {
            Debug.Log("도주 성공");
            DialogueManager.instance.PrintDialogue("ESC_001");
            Invoke("ScenesManager.instance.LoadTempMain()", 2.0f);
            return;
        }
        Debug.Log("도주 실패");
        DialogueManager.instance.PrintDialogue("ESC_002");
        return;
    }
}
