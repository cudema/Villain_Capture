using System;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthReporter
{
    event Action<float> ChangeHealth;

    void TakeDamage(float damage);
    float GetMaxHealth();
}

public enum Emotion { 증오 = 0, 경멸, 무관심, 흥미, 우호 }

public abstract class EnemyBase : MonoBehaviour, IHealthReporter
{
    [Header("이름")]
    [SerializeField]
    protected string enemyName;

    [Header("촬영 게이지")]
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

    [Header("감정 게이지")]
    [SerializeField]
    int minEmotionalGauge;
    [SerializeField]
    int maxEmotionalGauge;
    [SerializeField] //임시
    int emotionalGauge = 0;
    public int EmotionalGauge
    {
        get => emotionalGauge;
        set
        {
            emotionalGauge = Mathf.Clamp(value, minEmotionalGauge, maxEmotionalGauge);
            ChangedEmotionalGauge?.Invoke();
        }
    }
    [SerializeField]
    Emotion currentEmotion = Emotion.무관심;

    bool isEnage = false;

    [Header("색 변경 메테리얼")]
    [SerializeField]
    Material nomalMaterial;
    [SerializeField]
    Material parringableMaterial;

    [Header("패턴")]
    [SerializeField]
    protected PatternBase[] nomalPattern;
    [SerializeField]
    protected PatternBase[] enhancePattern;
    [SerializeField]
    protected PatternBase enagedPattern;
    PatternBase currentPattern;

    protected bool isParringable = false;
    bool isUesingEnagedPattern = false;

    List<PatternBase> patterns = new List<PatternBase>();
    int usePatternIndex = 0;

    GameObject wraning;
    Renderer enemyRenderer;

    Vector3 startPos;

    public event Action<float> ChangeHealth;
    public event Action ChangedEmotionalGauge;

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     currentPattern.StopPattern();
        // }
    }

    public void Setup()
    {
        startPos = transform.position;
        enemyRenderer = GetComponent<Renderer>();
        BattleManager.battlemanager.SetEnemy(this);
        BattleManager.OnSetEnemyTrun += SetPattern;
        BattleManager.OnEnemyTrun += StartPattern;
        BattleManager.EndEnemyTrun += ResetPosition;
        ChangedEmotionalGauge += OnChangeEmotion;
        patterns.AddRange(nomalPattern);
        wraning = transform.GetChild(0).gameObject;
        for (int i = 0; i < patterns.Count; i++)
        {
            patterns[i].Setup(this);
        }
        enagedPattern.Setup(this);
    }

    private void OnDisable()
    {
        BattleManager.OnEnemyTrun -= StartPattern;
    }

    protected virtual void SetPattern()
    {
        if (isEnage && !isUesingEnagedPattern)
        {
            isUesingEnagedPattern = true;
            usePatternIndex++;
            currentPattern = enagedPattern;
        }
        else
        {
            currentPattern = patterns[usePatternIndex++ % patterns.Count];
        }
        currentPattern.SetPattern();
    }
    
    public virtual void StartPattern()
    {
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
        float ranTemp = UnityEngine.Random.Range(0.0f, 0.2f);
        CurrentPhotoGauge += damage * (PlayerContoller.instance.GetJust() + ranTemp);
        BattleManager.battlemanager.StopAction();
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

    void OnChangeEmotion()
    {
        if (emotionalGauge >= 7)
        {
            currentEmotion = Emotion.우호;
            return;
        }
        if (emotionalGauge >= 4)
        {
            currentEmotion = Emotion.흥미;
            return;
        }
        if (emotionalGauge >= -3)
        {
            currentEmotion = Emotion.무관심;
            return;
        }
        if (emotionalGauge >= -6)
        {
            currentEmotion = Emotion.경멸;
            if (!isEnage)
            {
                isEnage = true;
                patterns.Clear();
                patterns.AddRange(enhancePattern);
                for (int i = 0; i < patterns.Count; i++)
                {
                    patterns[i].Setup(this);
                }
            }
            return;
        }
        currentEmotion = Emotion.증오;
        ChangedEmotionalGauge -= OnChangeEmotion;
    }

    public Emotion GetEnemyEmotion()
    {
        return currentEmotion;
    }

    public bool GetIsEnage()
    {
        return isEnage;
    }
}
