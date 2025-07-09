using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData player
    {
        private set; get;
    }

    [SerializeField]
    float damage;

    public float Damage
    {
        get { return damage; }
    }

    [SerializeField]
    float maxAttackJudgment;

    [SerializeField]
    float currentAttackJudgment = 0;
    public float CurrentAttackJudgment
    {
        get { return currentAttackJudgment; }
        set { currentAttackJudgment = Mathf.Clamp(value, 0f, maxAttackJudgment); }
    }


    private void Awake()
    {
        if (player == null)
        {
            player = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
