using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChangeKategori : MonoBehaviour
{
    [SerializeField]
    ToggleGroup group;
    [SerializeField]
    Toggle heal;
    [SerializeField]
    Toggle eq;

    void OnEnable()
    {
        InputManager.inputManager.tab.performed += ChangeKatefori;
    }

    void OnDisable()
    {
        InputManager.inputManager.tab.performed -= ChangeKatefori;
    }

    void ChangeKatefori(InputAction.CallbackContext value)
    {
        if (group.GetFirstActiveToggle() == heal)
        {
            eq.isOn = true;
        }
        else
        {
            heal.isOn = true;
        }
    }
}
