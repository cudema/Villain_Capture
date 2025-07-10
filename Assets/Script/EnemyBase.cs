using System;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthReporter
{
    event Action<float> ChangeHealth;

    void TakeDamage(float damage);
    float GetMaxHealth();
}

public abstract class EnemyBase : MonoBehaviour, IHealthReporter
{
    [Header("이름")]
    [SerializeField]
    protected string enemyName;

    [Header("게이지")]
    [SerializeField]
    protected float maxPhotoGauge;
    protected float currentPhotoGauge = 0;
    public float CurrentPhotoGauge
    {
        get => currentPhotoGauge;
        set
        {
            currentPhotoGauge = Mathf.Clamp(value, 0, maxPhotoGauge);
            ChangeHealth?.Invoke(CurrentPhotoGauge);
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
            currentAngerGauge = Mathf.Clamp(value, 0, maxAngerGauge);
            ChangedAngerGauge?.Invoke();
        }
    }

    [Header("공격 패턴")]
    [SerializeField]
    protected PatternBase[] nomalPattern;
    [SerializeField]
    protected PatternBase[] spacialPattern;
    [SerializeField]
    protected PatternBase enagedPattern;

    List<PatternBase> patterns = new List<PatternBase>();
    int usePatternIndex = 0;

    GameObject wraning;

    public event Action<float> ChangeHealth;
    public event Action ChangedAngerGauge;

    public void Setup()
    {
        patterns.AddRange(nomalPattern);
        patterns.AddRange(spacialPattern);
        BattleManager.SetEnemy(this);
        BattleManager.OnEnemyTrun += StartPattern;
        wraning = transform.GetChild(0).gameObject;
        for (int i = 0; i < patterns.Count; i++)
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
        patterns[usePatternIndex++ % patterns.Count].StartPattern();
    }

    public void OnWraning()
    {
        wraning.SetActive(true);
    }

    public void OffWraning()
    {
        wraning.SetActive(false);
    }

    public void SetWraningScale(float scale)
    {
        wraning.transform.localScale = Vector3.one * scale;
    }

    public void SetWraningScale(Vector3 scale)
    {
        wraning.transform.localScale = scale;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(PlayerContoller.instance.GetCurrentAttackJudgment());
        CurrentPhotoGauge += damage * (PlayerContoller.instance.GetCurrentAttackJudgment());
    }

    public float GetMaxHealth()
    {
        return maxPhotoGauge;
    }
}
