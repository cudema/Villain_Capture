using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    NodePattern pattern;

    void startPattern()
    {
        BattleManager.PatternStart(pattern);
    }
}
