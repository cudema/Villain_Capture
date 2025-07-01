using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Trun { 아군 = 0, 적 }

public class TrunManager : MonoBehaviour
{
    [SerializeField]
    Trun currentTrun = Trun.아군;
    InputManager input;

    private void Start()
    {
        input = GameObject.Find("PlayerInputManager").GetComponent<InputManager>();
        StartCoroutine(TrunFlow());
    }

    public void ChangeTrun(Trun newTrun)
    {
        if (currentTrun == newTrun)
        {
            return;
        }

        currentTrun = newTrun;
    }

    IEnumerator TrunFlow()
    {
        while (true)
        {
            input.ChangeBattleUIInput();
            yield return new WaitUntil(() => currentTrun == Trun.적);
            input.ChangeBattleMoveInput();
            yield return new WaitUntil(() => currentTrun == Trun.아군);
        }
    }
}
