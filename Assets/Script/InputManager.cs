using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager inputManager
    {
        get; private set;
    }
    PlayerInput input;
    [SerializeField]
    static BattleSeleterBase selecter;
    [SerializeField]
    PlayerContoller contoller;

    InputActionMap battleMenuActionMap;
    InputActionMap battleMoveActionMap;
    InputActionMap battlePhotoActionMap;

    public InputAction changeSelect;
    public InputAction select;
    public InputAction move;
    public InputAction parring;
    public InputAction photo;
    public InputAction rotationCamera;
    public InputAction zoom;
    public InputAction focus;

    InputActionMap currentActionMap;
    InputActionMap map;

    private void Awake()
    {
        if (inputManager == null)
        {
            inputManager = this;
        }
        else
        {
            Destroy(this);
        }

        input = GetComponent<PlayerInput>();
        input.SwitchCurrentActionMap("BattleMenu");

        //현재 임시로 넣어둔 코드 수정 필요
        //전투 시작시 구독, 전투 종료시 해지
        BattleManager.OnPlayerTrun += ChangeBattleUIInput;
        BattleManager.OnEnemyTrun += ChangeBattleMoveInput;
        BattleManager.EndPlayerAction += ChangeBattleBeforeInput;
        //-------------------------------------------------------------------

        battleMenuActionMap = input.actions.FindActionMap("BattleMenu");
        if (battleMenuActionMap != null)
        {
            changeSelect = battleMenuActionMap.FindAction("ChangeSelect");
            select = battleMenuActionMap.FindAction("Select");
        }
        battleMoveActionMap = input.actions.FindActionMap("BattleMove");
        if (battleMoveActionMap != null)
        {
            move = battleMoveActionMap.FindAction("Move");
            parring = battleMoveActionMap.FindAction("Parring");
        }
        battlePhotoActionMap = input.actions.FindActionMap("BattlePhoto");
        if (battlePhotoActionMap != null)
        {
            photo = battlePhotoActionMap.FindAction("Photo");
            rotationCamera = battlePhotoActionMap.FindAction("RotationCamera");
            zoom = battlePhotoActionMap.FindAction("Zoom");
            focus = battlePhotoActionMap.FindAction("Focus");
        }
    }

    void OnDestroy()
    {
        BattleManager.OnPlayerTrun -= ChangeBattleUIInput;
        BattleManager.OnEnemyTrun -= ChangeBattleMoveInput;
        BattleManager.EndPlayerAction -= ChangeBattleBeforeInput;
    }

    private void OnEnable()
    {
        changeSelect.performed += OnChangeSelect;
        select.performed += OnSelect;
        move.performed += OnMove;
        move.canceled += OnMove;

        battleMenuActionMap.Disable();
        battleMoveActionMap.Disable();
        battlePhotoActionMap.Disable();
        currentActionMap = battleMenuActionMap;
        ChangeBattleUIInput();
    }

    public void ChangeBattleUIInput()
    {
        map = currentActionMap;
        currentActionMap.Disable();
        currentActionMap = battleMenuActionMap;
        currentActionMap.Enable();
    }

    public void ChangeBattleMoveInput()
    {
        map = currentActionMap;
        currentActionMap.Disable();
        currentActionMap = battleMoveActionMap;
        currentActionMap.Enable();
    }

    public void ChangeBattlePhotoInput()
    {
        map = currentActionMap;
        currentActionMap.Disable();
        currentActionMap = battlePhotoActionMap;
        currentActionMap.Enable();
    }

    public void ChangeBattleBeforeInput()
    {
        currentActionMap.Disable();
        currentActionMap = map;
        currentActionMap.Enable();
    }

    public void ChangeBattleNonInput()
    {
        map = currentActionMap;
        currentActionMap.Disable();
    }

    public void OnChangeSelect(InputAction.CallbackContext value)
    {
        selecter.ChangeBattleAction(value.ReadValue<Vector2>());
    }

    public void OnSelect(InputAction.CallbackContext value)
    {
        selecter.SelectButton();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 temp = value.ReadValue<Vector2>();
        contoller.SetDirection(temp);
    }

    public static void ChangeSelecter(BattleSeleterBase newSeleter)
    {
        selecter = newSeleter;
    }
}
