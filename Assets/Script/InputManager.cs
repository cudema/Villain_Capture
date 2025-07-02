using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput input;
    [SerializeField]
    BattleMainSeleter selecter;
    [SerializeField]
    PlayerContoller contoller;

    InputActionMap map;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.SwitchCurrentActionMap("BattleMenu");

        //현재 임시로 넣어둔 코드 수정 필요
        //전투 시작시 구독, 전투 종료시 해지
        BattleManager.OnPlayerTrun += ChangeBattleUIInput;
        BattleManager.OnEnemyTrun += ChangeBattleMoveInput;
        BattleManager.OnPlayerAction += ChangeBattleNonInput;
        BattleManager.EndPlayerAction += ChangeBattleBeforeInput;
    }

    void ChangeBattleUIInput()
    {
        input.SwitchCurrentActionMap("BattleMenu");
    }

    void ChangeBattleMoveInput()
    {
        input.SwitchCurrentActionMap("BattleMove");
    }

    void ChangeBattleNonInput()
    {
        map = input.currentActionMap;
        input.SwitchCurrentActionMap("Menu");
    }

    void ChangeBattleBeforeInput()
    {
        input.currentActionMap = map;
        map = null;
    }

    public void OnChangeSelect(InputValue value)
    {
        selecter.ChangeBattleAction(value.Get<float>());
    }

    public void OnSelect(InputValue value)
    {
        selecter.SelectButton();
    }

    public void OnMove(InputValue value)
    {
        Vector2 temp = value.Get<Vector2>();
        contoller.SetDirection(temp);
    }
}
