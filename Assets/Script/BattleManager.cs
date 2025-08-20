using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public enum Trun { 아군 = 0, 적 }

public class BattleManager : MonoBehaviour
{
    //임시
    [HideInInspector]
    public bool isOnEnemy;
    [HideInInspector]
    public bool isEndPlayerAction = true;
    Trun currentTrun = Trun.아군;
    int currentAction = -1;
    public static BattleManager battlemanager
    {
        get; private set;
    }
    public BattleRunConttorl runConttorl;
    EnemyBase currentEnemy = null;
    public EnemyBase CurrentEnemy
    {
        get => currentEnemy;
        private set => currentEnemy = value;
    }
    public int turnCount
    {
        private set; get;
    }
    [Header("턴 사이 시간")]
    [SerializeField]
    float trunDelay;
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
    public static event Action OnSetEnemyTrun;
    public static event Action EndSetEnemyTrun;
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ResetFild();
        UICSVLoader.SetUICSV();
        ItemCSVLoader.SetItemCSV();
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        EndSetEnemyTrun += ResetFild;
        OnPlayerTrun += AddTurn;
    }

    private void Update()
    {

    }

    public void ChangeTrun(Trun newTrun)
    {
        if (currentTrun == newTrun)
        {
            return;
        }
        isOnEnemy = false;
        currentTrun = newTrun;
        switch (currentTrun)
        {
            case Trun.아군:
                StartCoroutine(OnPlayerTrunCorutine());
                break;
            case Trun.적:
                StartCoroutine(OnEnemyTrunCorutine());
                break;
            default:
                break;
        }
    }

    IEnumerator OnPlayerTrunCorutine()
    {
        isOnEnemy = false;
        EndSetEnemyTrun?.Invoke();
        yield return null;
        EndEnemyTrun?.Invoke();
        yield return new WaitUntil(() => isOnEnemy);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        OnPlayerTrun?.Invoke();
    }

    IEnumerator OnEnemyTrunCorutine()
    {
        isOnEnemy = false;
        EndPlayerTrun?.Invoke();
        yield return null;
        OnSetEnemyTrun?.Invoke();
        yield return new WaitUntil(() => isOnEnemy);
        yield return new WaitForSeconds(trunDelay);
        OnEnemyTrun?.Invoke();
    }

    public void PlayerAction(int newAction)
    {
        currentAction = newAction;

        if (newAction != -1)
        {
            StartCoroutine(InAction());
        }
    }

    IEnumerator InAction()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        OnPlayerAction?.Invoke();
        yield return new WaitUntil(() => currentAction == -1);
        EndPlayerAction?.Invoke();
        yield return new WaitUntil(() => isEndPlayerAction);
        ChangeTrun(Trun.적);
    }

    public void SetEnemy(EnemyBase newEnemy)
    {
        currentEnemy = newEnemy;
        turnCount = 0;
        currentTrun = Trun.아군;
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

    public IEnumerator ActionTimer(float time)
    {
        yield return new WaitForSeconds(time);

        StopAction();
    }

    void EscapeBattle()
    {
        currentEnemy = null;
        StopAllCoroutines();
    }

    public void BattleLose()
    {
        EscapeBattle();
        ScenesManager.instance.LoadLose();
    }

    public void BattleWin()
    {
        EscapeBattle();
        ScenesManager.instance.LoadWin();
    }

    public void BattleBack()
    {
        EscapeBattle();
        ScenesManager.instance.LoadTempMain();
    }
}
