using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Trun { 아군 = 0, 적 }

public class BattleManager : MonoBehaviour
{
    static Trun currentTrun = Trun.아군;
    static BattleAction currentAction = BattleAction.초기화;
    InputManager input;
    static BattleManager battlemanager;
    static SpawnObject spawner;

    public static event Action OnEnemyTrun;
    public static event Action EndEnemyTrun;

    public static event Action OnPlayerTrun;
    public static event Action EndPlayerTrun;

    public static event Action OnPlayerAction;
    public static event Action EndPlayerAction;

    private void Awake()
    {
        if (battlemanager == null)
        {
            battlemanager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        input = GameObject.Find("PlayerInputManager").GetComponent<InputManager>();
        spawner = transform.GetComponentInChildren<SpawnObject>();
    }

    private void Start()
    {
        spawner.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeTrun(Trun.아군);
        }
    }

    public static void ChangeTrun(Trun newTrun)
    {
        if (currentTrun == newTrun)
        {
            return;
        }

        currentTrun = newTrun;
        switch (currentTrun)
        {
            case Trun.아군:
                EndEnemyTrun?.Invoke();
                OnPlayerTrun?.Invoke();
                break;
            case Trun.적:
                EndPlayerTrun?.Invoke();
                OnEnemyTrun?.Invoke();
                break;
            default:
                break;
        }
    }

    public static void PlayerAction(BattleAction newAction)
    {
        currentAction = newAction;

        if (newAction != BattleAction.초기화)
        {
            battlemanager.StartCoroutine(InAction());
        }
    }

    static IEnumerator InAction()
    {
        OnPlayerAction?.Invoke();
        yield return new WaitUntil(() => currentAction == BattleAction.초기화);
        EndPlayerAction?.Invoke();
        ChangeTrun(Trun.적);
    }

    public static void PatternStart(NodePattern pattern)
    {
        spawner.SpawnObj(pattern);
    }
}
