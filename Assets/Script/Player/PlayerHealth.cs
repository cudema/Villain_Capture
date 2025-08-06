using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthReporter
{
    [SerializeField]
    int maxHP;
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
            BattleManager.battlemanager.EscapeBattle();
            ScenesManager.instance.LoadTempMain();//임시로 만든거
            Debug.Log("죽음");
        }
    }
}
