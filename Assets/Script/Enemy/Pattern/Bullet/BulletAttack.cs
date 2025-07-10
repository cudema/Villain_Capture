using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    float damage;

    public void SetDamage(float newDamage)
    {
        this.damage = newDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IHealthReporter>().TakeDamage(damage);
        }
    }
}
