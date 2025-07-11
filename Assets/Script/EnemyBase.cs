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
    int minEmotionalGauge;
    [SerializeField]
    int maxEmotionalGauge;
    int emotionalGauge = 0;
    public int EmotionalGauge
    {
        get => emotionalGauge;
        set
        {
            emotionalGauge = Mathf.Clamp(value, minEmotionalGauge, maxEmotionalGauge);
        }
    }

    [Header("색 변경 샘플")]
    [SerializeField]
    Material nomalMaterial;
    [SerializeField]
    Material parringableMaterial;

    [Header("공격 패턴")]
    [SerializeField]
    protected PatternBase[] nomalPattern;
    [SerializeField]
    protected PatternBase enagedPattern;
    PatternBase currentPattern;

    protected bool isParringable = false;

    List<PatternBase> patterns = new List<PatternBase>();
    int usePatternIndex = 0;

    GameObject wraning;
    Renderer enemyRenderer;

    Vector3 startPos;

    public event Action<float> ChangeHealth;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentPattern.StopPattern();
        }
    }

    public void Setup()
    {
        startPos = transform.position;
        enemyRenderer = GetComponent<Renderer>();
        patterns.AddRange(nomalPattern);
        BattleManager.SetEnemy(this);
        BattleManager.OnEnemyTrun += StartPattern;
        BattleManager.EndEnemyTrun += ResetPosition;
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
        currentPattern = patterns[usePatternIndex++ % patterns.Count];
        currentPattern.StartPattern();
    }

    public void OnWraning()
    {
        wraning.SetActive(true);
    }

    public void OffWraning()
    {
        wraning.SetActive(false);
    }

    public void OnRenderer()
    {
        enemyRenderer.enabled = true;
    }

    public void OffRenderer()
    {
        enemyRenderer.enabled = false;
    }

    void ResetPosition()
    {
        OffWraning();
        OnRenderer();
        transform.position = startPos;
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
        float ranTemp = UnityEngine.Random.Range(0.0f, 0.2f);
        CurrentPhotoGauge += damage * (PlayerContoller.instance.GetCurrentAttackJudgment() + ranTemp);
    }

    public float GetMaxHealth()
    {
        return maxPhotoGauge;
    }

    public void OnParringable()
    {
        enemyRenderer.material = parringableMaterial;
        isParringable = true;
    }

    public void OffParringable()
    {
        enemyRenderer.material = nomalMaterial;
        isParringable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isParringable && other.CompareTag("ParringPoint"))
        {
            currentPattern.StopPattern();
            OffParringable();
        }
    }
}
