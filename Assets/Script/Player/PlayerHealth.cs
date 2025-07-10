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
        }
    }

    public event Action<float> ChangeHealth;

    private void Awake()
    {

    }

    public float GetMaxHealth()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}
