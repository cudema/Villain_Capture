using System;
using System.Collections;
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
    public string enemyName;

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
    protected int minEmotionalGauge;
    [SerializeField]
    protected int maxEmotionalGauge;
    protected int emotionalGauge = 0;
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
    protected Emotion currentEmotion = Emotion.무관심;

    protected bool isEnage = false;

    [Header("색 변경 메테리얼")]
    [SerializeField]
    protected Material nomalMaterial;
    [SerializeField]
    protected Material parringableMaterial;
    [SerializeField]
    protected GameObject model;

    [Header("패턴")]
    [SerializeField]
    protected PatternBase[] nomalPattern;
    [SerializeField]
    protected PatternBase[] enhancePattern;
    [SerializeField]
    protected PatternBase enagedPattern;
    protected PatternBase currentPattern;

    protected float attackTime;

    [Header("대사 코드")]
    [SerializeField]
    string dialogueID;

    protected bool isParringable = false;
    bool isUesingEnagedPattern = false;

    [HideInInspector]
    public Animator animator;

    protected List<PatternBase> patterns = new List<PatternBase>();
    protected int usePatternIndex = 0;
    protected BulletAttack attack;
    protected Transform wraning;
    protected Renderer enemyRenderer;

    protected Vector3 startPos;

    public event Action<float> ChangeHealth;
    public event Action ChangedEmotionalGauge;

    IEnumerator bingAttack;

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     currentPattern.StopPattern();
        // }
    }

    public void Setup()
    {
        animator = GetComponentInChildren<Animator>();
        startPos = transform.position;
        enemyRenderer = GetComponent<Renderer>();
        //BattleManager.battlemanager.SetEnemy(this);
        BattleManager.EndPlayerTrun += SetPattern;
        BattleManager.OnEnemyTrun += StartPattern;
        BattleManager.EndEnemyTrun += ResetPosition;
        ChangedEmotionalGauge += OnChangeEmotion;
        patterns.AddRange(nomalPattern);
        wraning = transform.GetChild(0);
        attack = GetComponent<BulletAttack>();
        for (int i = 0; i < patterns.Count; i++)
        {
            patterns[i].Setup(this);
        }
        enagedPattern.Setup(this);
        bingAttack = Attack();
    }

    private void OnDisable()
    {
        BattleManager.OnEnemyTrun -= StartPattern;
        BattleManager.EndPlayerTrun -= SetPattern;
        BattleManager.EndEnemyTrun -= ResetPosition;
        ChangedEmotionalGauge -= OnChangeEmotion;
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
        wraning.GetChild(0).gameObject.SetActive(true);
    }

    public void OffWraning()
    {
        wraning.GetChild(0).gameObject.SetActive(false);
    }

    public void OnRenderer()
    {
        //enemyRenderer.enabled = true;
        //GetComponent<Collider>().enabled = true;
        model.SetActive(true);
    }

    public void OffRenderer()
    {
        //enemyRenderer.enabled = false;
        //GetComponent<Collider>().enabled = false;
        model.SetActive(false);
    }

    void ResetPosition()
    {
        OffWraning();
        OnRenderer();
        StopCoroutine(bingAttack);
        if (transform.position != startPos)
        {
            StartCoroutine(PositionReset());
        }
    }

    protected virtual IEnumerator PositionReset()
    {
        yield return null;
        transform.position = startPos;
    }

    public void SetWraningScale(float scale)
    {
        wraning.localScale = new Vector3(scale, scale, 1);
    }

    public void SetWraningScale(Vector3 scale)
    {
        wraning.localScale = scale;
    }

    public void TakeDamage(float damage)
    {
        float justTemp = PlayerContoller.instance.GetJust();
        if (justTemp == 0)
        {
            BattleManager.battlemanager.StopAction();
            return;
        }
        float ranTemp = UnityEngine.Random.Range(0.0f, 0.2f);
        CurrentPhotoGauge += damage * (justTemp + ranTemp);
        BattleManager.battlemanager.StopAction();
    }

    public float GetMaxHealth()
    {
        return maxPhotoGauge;
    }

    public void OnParringable()
    {
        //enemyRenderer.material = parringableMaterial;
        isParringable = true;
    }

    public void OffParringable()
    {
        //enemyRenderer.material = nomalMaterial;
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

    public string GetDialogueID()
    {
        return dialogueID;
    }

    public void SetAttack(float damage, float attackTime)
    {
        attack.SetDamage(damage);
        this.attackTime = attackTime;
    }

    public void OnAttack()
    {
        StopCoroutine(bingAttack);
        bingAttack = Attack();
        StartCoroutine(bingAttack);
    }

    IEnumerator Attack()
    {
        wraning.GetChild(1).gameObject.SetActive(true);

        yield return new WaitForSeconds(attackTime);

        wraning.GetChild(1).gameObject.SetActive(false);
        yield break;
    }
}
