using System;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("이름")]
    [SerializeField]
    protected string enemyName;

    [Header("게이지")]
    [SerializeField]
    protected float maxPhotoGauge;
    protected float currentPhotoGauge;
    public float CurrentPhotoGauge
    {
        get => currentPhotoGauge;
        set
        {
            currentPhotoGauge = Mathf.Clamp(currentPhotoGauge + value, 0, maxPhotoGauge);
            ChangedPhotoGauge?.Invoke();
            if (currentPhotoGauge == maxPhotoGauge)
            {
                PhotoGaugeReachedMax?.Invoke();
            }
        }
    }

    [SerializeField]
    protected float maxAngerGauge;
    protected float currentAngerGauge;
    public float CurrentAngerGauge
    {
        get => currentAngerGauge;
        set
        {
            currentAngerGauge = Mathf.Clamp(currentPhotoGauge + value, 0, maxAngerGauge);
            ChangedAngerGauge?.Invoke();
            if (currentAngerGauge == maxAngerGauge)
            {
                AngerGaugeReachedMax?.Invoke();
            }
        }
    }

    [Header("공격 패턴")]
    [SerializeField]
    protected PatternBase[] patterns;

    public event Action ChangedPhotoGauge;
    public event Action PhotoGaugeReachedMax;
    public event Action ChangedAngerGauge;
    public event Action AngerGaugeReachedMax;

    public void Setup()
    {
        BattleManager.SetEnemy(this);
        BattleManager.OnEnemyTrun += StartPattern;
        for (int i = 0; i < patterns.Length; i++)
        {
            patterns[i].Setup(this);
        }
    }

    private void OnDisable()
    {
        BattleManager.OnEnemyTrun -= StartPattern;
    }

    public virtual void StartPattern()
    {
        int temp = UnityEngine.Random.Range(0, patterns.Length);
        patterns[temp].StartPattern();
    }
}
