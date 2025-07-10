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
        ChangeHealth += PrintCurrentHP;
        currentHP = maxHP;
    }

    public float GetMaxHealth()
    {
        return maxHP;
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= (int)damage;
        if (CurrentHP <= 0 )
        {
            Debug.Log("Á×À½");
        }
    }

    void PrintCurrentHP(float currentHP)
    {
        Debug.Log($"{currentHP}");
    }
}
