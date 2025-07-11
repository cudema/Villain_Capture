using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public static PlayerContoller instance;

    PlayerMovement movement;
    PlayerAttack attack;
    PlayerHealth health;
    PlayerParing parring;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<PlayerHealth>();
        parring = GetComponent<PlayerParing>();
    }

    private void Start()
    {
        BattleManager.EndEnemyTrun += movement.ReturnPosition;
        BattleManager.OnEnemyTrun += movement.ReturnPosition;
        InputManager.inputManager.parring.performed += parring.OnParring;
    }

    private void Update()
    {
        movement.ToMove();
    }

    public void SetDirection(Vector2 vector)
    {
        movement.SetDirection(vector);
    }

    public float GetCurrentAttackJudgment()
    {
        return attack.CurrentAttackJudgment;
    }

    public void SetCurrentAttackJudgment(float newAttackJudgment)
    {
        attack.CurrentAttackJudgment = newAttackJudgment;
    }

    public void StartPattern()
    {
        attack.SetPattern();
    }

    public void Attack()
    {
        attack.Attack();
    }

    public float GetMaxHealth()
    {
        return health.GetMaxHealth();
    }
}
