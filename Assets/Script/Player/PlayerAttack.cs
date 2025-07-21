using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("�Կ�")]
    [SerializeField]
    NodePattern pattern;

    [SerializeField]
    float damage;

    [SerializeField]
    float maxAttackJudgment;
    float currentAttackJudgment = 0;

    public float CurrentAttackJudgment
    {
        get { return currentAttackJudgment; }
        set { currentAttackJudgment = Mathf.Clamp(value, 0f, maxAttackJudgment); }
    }

    public void Attack()
    {
        BattleManager.CurrentEnemy.TakeDamage(damage);
        CurrentAttackJudgment = 0;
    }

    public void SetPattern()
    {
        BattleManager.PatternStart(pattern);
    }
}
