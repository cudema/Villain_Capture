using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Trun { 아군 = 0, 적 }

public class BattleManager : MonoBehaviour
{
    static Trun currentTrun = Trun.아군;
    static int currentAction = -1;
    InputManager input;
    static BattleManager battlemanager;
    static SpawnObject spawner;
    static EnemyBase currentEnemy = null;
    public static EnemyBase CurrentEnemy
    {
        get { return currentEnemy; }
        private set => currentEnemy = value;
    }

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

    public static void PlayerAction(int newAction)
    {
        currentAction = newAction;

        if (newAction != -1)
        {
            battlemanager.StartCoroutine(InAction());
        }
    }

    static IEnumerator InAction()
    {
        OnPlayerAction?.Invoke();
        yield return new WaitUntil(() => currentAction == -1);
        EndPlayerAction?.Invoke();
        ChangeTrun(Trun.적);
    }

    public static void PatternStart(NodePattern pattern)
    {
        spawner.SpawnObj(pattern);
    }

    public static void SetEnemy(EnemyBase newEnemy)
    {
        currentEnemy = newEnemy;
    }
}
