using NUnit.Framework.Internal.Filters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayMode { 일반 = 0, 플렛포머}

public class PlayerContoller : MonoBehaviour
{
    public static PlayerContoller instance;
    [SerializeField]
    TempRawImage flash;
    PlayerMovement movement;
    PlayerAttack attack;
    [HideInInspector]
    public PlayerHealth health;
    PlayerParing parring;
    [HideInInspector]
    public Filming filming;

    bool isMoveable = false;

    [SerializeField]
    PlayMode currentState = PlayMode.일반;

    [SerializeField]
    public GameObject nomalModel;
    [SerializeField]
    public GameObject moveModel;
    [HideInInspector]
    public Animator nomalAnimator;
    [HideInInspector]
    public Animator moveAnimator;

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
        filming = GetComponent<Filming>();
        nomalAnimator = nomalModel.GetComponent<Animator>();
        moveAnimator = moveModel.GetComponent<Animator>();
    }

    private void Start()
    {
        BattleManager.EndEnemyTrun += movement.ReturnPosition;
        BattleManager.EndEnemyTrun += OffMoveable;
        BattleManager.OnSetEnemyTrun += movement.StartMovePosition;
        BattleManager.OnSetEnemyTrun += OnMoveavle;
        InputManager.inputManager.parring.performed += parring.OnParring;
        InputManager.inputManager.zoom.performed += filming.OnChangeZoom;
        InputManager.inputManager.focus.performed += filming.OnChangeFocus;
        InputManager.inputManager.photo.performed += OnFilming;
        //InputManager.inputManager.focus.canceled += filming.OnChangeFocus;
        InputManager.inputManager.rotationCamera.performed += filming.OnChangeRotation;
        InputManager.inputManager.move.performed += OnMoveAnimation;
        InputManager.inputManager.move.canceled += StopMoveAnimation;
    }

    void OnDisable()
    {
        BattleManager.EndEnemyTrun -= movement.ReturnPosition;
        BattleManager.EndEnemyTrun -= OffMoveable;
        BattleManager.OnSetEnemyTrun -= movement.StartMovePosition;
        BattleManager.OnSetEnemyTrun -= OnMoveavle;
        InputManager.inputManager.parring.performed -= parring.OnParring;
        InputManager.inputManager.zoom.performed -= filming.OnChangeZoom;
        InputManager.inputManager.focus.performed -= filming.OnChangeFocus;
        InputManager.inputManager.photo.performed -= OnFilming;
        //InputManager.inputManager.focus.canceled -= filming.OnChangeFocus;
        InputManager.inputManager.rotationCamera.performed -= filming.OnChangeRotation;
        InputManager.inputManager.move.performed -= OnMoveAnimation;
        InputManager.inputManager.move.canceled -= StopMoveAnimation;
    }

    private void Update()
    {
        if (isMoveable)
        {
            switch (currentState)
            {
                case PlayMode.일반:
                    movement.ToMove();
                    moveAnimator.SetBool("IsF", false);
                    break;
                case PlayMode.플렛포머:
                    movement.ToJumpMove();
                    moveAnimator.SetBool("IsF", true);
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
        //attack.SetPattern();
        filming.SetPerfactDistance();
        filming.OnFilming();
    }

    void OnMoveavle()
    {
        isMoveable = true;
    }

    void OffMoveable()
    {
        isMoveable = false;
    }

    public void ChangePlayMode(PlayMode play)
    {
        currentState = play;
    }

    public PlayMode GetPlayMode()
    {
        return currentState;
    }

    public float GetJust()
    {
        if (Mathf.Abs(filming.justFocus) > 1 || !filming.justRotate)
        {
            return 0;
        }
        float temp = 1f - (Mathf.Abs(filming.justFocus) * 0.3f);
        if (filming.justZoom && filming.justFocus == 0)
        {
            temp += 0.2f;
        }
        return temp;
    }

    public void HealPlayer(float point, float percentPoint)
    {
        health.Heal(point, percentPoint);
    }

    public void OnAttack()
    {
        attack.Attack();
    }

    public void OnFilming(InputAction.CallbackContext value)
    {
        InputManager.inputManager.ChangeBattleNonInput();
        flash.OnFilming();
    }

    public void OnMoveAnimation(InputAction.CallbackContext value)
    {
        moveAnimator.SetBool("IsMove", true);
    }

    public void StopMoveAnimation(InputAction.CallbackContext value)
    {
        moveAnimator.SetBool("IsMove", false);
    }
}
