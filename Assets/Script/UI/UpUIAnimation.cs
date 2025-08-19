using UnityEngine;

public class UpUIAnimation : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        BattleManager.OnPlayerTrun += PlayUpAnimation;
        BattleManager.EndPlayerTrun += PlayDownAnimation;
    }

    void OnDisable()
    {
        BattleManager.OnPlayerTrun -= PlayUpAnimation;
        BattleManager.EndPlayerTrun -= PlayDownAnimation;
    }

    public void PlayDownAnimation()
    {
        animator.Play("OffUI");
    }

    public void PlayUpAnimation()
    {
        animator.Play("OnUI");
    }
}
