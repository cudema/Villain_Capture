using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("촬영")]
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

    public void Attack(InputAction.CallbackContext value)
    {
        BattleManager.battlemanager.CurrentEnemy.TakeDamage(damage);
    }

    public void SetPattern()
    {
        BattleManager.battlemanager.PatternStart(pattern);
    }
}
