using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public enum Trun { 아군 = 0, 적 }

public class BattleManager : MonoBehaviour
{
    static Trun currentTrun = Trun.아군;
    static int currentAction = -1;
    public static BattleManager battlemanager
    {
        get; private set;
    }
    public BattleRunConttorl runConttorl;
    static SpawnObject spawner;
    static EnemyBase currentEnemy = null;
    public static EnemyBase CurrentEnemy
    {
        get => currentEnemy; 
        private set => currentEnemy = value;
    }
    public int turnCount
    {
        private set; get;
    }
    [Header("중앙 이동 방경")]
    [SerializeField]
    Vector2 center;
    Vector2 currentCenter;
    public Vector2 Center
    {
        get { return currentCenter; }
        private set { currentCenter = value; }
    }
    [SerializeField]
    Vector2 radius;
    Vector2 currentRadius;
    public Vector2 Radius
    {
        get { return currentRadius; }
        private set { currentRadius = value; }
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

        spawner = transform.GetComponentInChildren<SpawnObject>();
        ResetFild();
        UICSVLoader.SetUICSV();
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        EndEnemyTrun += ResetFild;
        spawner.gameObject.SetActive(false);
        OnPlayerTrun += AddTurn;
    }

    private void Update()
    {

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
        Debug.Log(0);
        yield return new WaitUntil(() => currentAction == -1);
        EndPlayerAction?.Invoke();
        Debug.Log(1);
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

    public float GetJudgmentFloat()
    {
        return 0;
    }

    public void ChangeFild(Vector2 newCenter, Vector2 newRadius)
    {
        Center = newCenter;
        Radius = newRadius;
    }

    void ResetFild()
    {
        Center = center;
        Radius = radius;
    }

    public void StopAction()
    {
        currentAction = -1;
    }

    void AddTurn()
    {
        turnCount++;
        Debug.Log(turnCount);
    }
}
