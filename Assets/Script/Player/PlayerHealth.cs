using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthReporter
{
    [SerializeField]
    int maxHP;
    [SerializeField]
    float invincibleTime;
    [SerializeField]
    float blinkingTime;
    int currentHP;
    public int CurrentHP
    {
        get => currentHP;
        set
        {
            currentHP = Mathf.Clamp(value, 0, maxHP);
            ChangeHealth?.Invoke(currentHP);
        }
    }

    public event Action<float> ChangeHealth;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public float GetMaxHealth()
    {
        return maxHP;
    }

    public void Heal(float healPoint, float healPercentPoint)
    {
        CurrentHP += (int)healPoint + (int)(maxHP * healPercentPoint);
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= (int)damage;
        PlayerContoller.instance.filming.OnSettingChange();
        if (CurrentHP <= 0)
        {
            BattleManager.battlemanager.BattleLose();
            Debug.Log("죽음");
            return;
        }

        StartCoroutine(NoHitTime());
    }

    public void EndNoHitTime()
    {
        StopAllCoroutines();
        GetComponent<Collider>().enabled = true;
    }

    IEnumerator NoHitTime()
    {
        GetComponent<Collider>().enabled = false;
        float time = Time.time;
        while (invincibleTime > Time.time - time)
        {
            PlayerContoller.instance.moveModel.SetActive((Time.time - time) % blinkingTime < blinkingTime / 2 ? true : false);
            yield return null;
        }
        PlayerContoller.instance.moveModel.SetActive(true);

        GetComponent<Collider>().enabled = true;

        yield break;
    }
}
