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
        if (BattleManager.CurrentEnemy.GetIsEnage())
        {
            temp += enageProbability;
        }
        if (BattleManager.battlemanager.turnCount >= bonusTurnCount)
        {
            temp += turnProbability;
        }

        float tempRandom = Random.Range(0.0f, 1.0f);
        Debug.Log(tempRandom);
        if (temp > tempRandom)
        {
            Debug.Log("성공");
            BattleManager.battlemanager.StopAction();
            return;
        }
        Debug.Log("실패");
        BattleManager.battlemanager.StopAction();
        return;
    }
}
