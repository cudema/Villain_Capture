using UnityEngine;

public enum PlayMode { 기본 = 0, 플렛포머}

public class PlayerContoller : MonoBehaviour
{
    public static PlayerContoller instance;

    PlayerMovement movement;
    PlayerAttack attack;
    PlayerHealth health;
    PlayerParing parring;

    bool isMoveable = false;

    [SerializeField]
    PlayMode currentState = PlayMode.기본;

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
        BattleManager.EndEnemyTrun += OffMoveable;
        BattleManager.OnEnemyTrun += movement.StartMovePosition;
        BattleManager.OnEnemyTrun += OnMoveavle;
        InputManager.inputManager.parring.performed += parring.OnParring;
    }

    private void Update()
    {
        if (isMoveable)
        {
            switch (currentState)
            {
                case PlayMode.기본:
                    movement.ToMove();
                    break;
                case PlayMode.플렛포머:
                    movement.ToJumpMove();
                    break;
                default:
                    break;
            }
        }
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

    void OnMoveavle()
    {
        isMoveable = true;
    }

    void OffMoveable()
    {
        isMoveable = false;
    }

    public float GetMaxHealth()
    {
        return health.GetMaxHealth();
    }

    public void ChangePlayMode(PlayMode play)
    {
        currentState = play;
    }

    public PlayMode GetPlayMode()
    {
        return currentState;
    }
}
