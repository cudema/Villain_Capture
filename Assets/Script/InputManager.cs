using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput input;
    [SerializeField]
    BattleMainSeleter selecter;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.SwitchCurrentActionMap("BattleMenu");
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void ChangeBattleUIInput()
    {
        input.SwitchCurrentActionMap("BattleMenu");
    }

    public void ChangeBattleMoveInput()
    {
        input.SwitchCurrentActionMap("BattleMove");
    }

    public void OnChangeSelect(InputValue value)
    {
        selecter.ChangeBattleAction(value.Get<float>());
    }

    public void OnSelect(InputValue value)
    {
        selecter.SelectButton();
    }
}
