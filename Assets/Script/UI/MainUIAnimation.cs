using UnityEngine;

public class MainUIAnimation : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        BattleManager.OnPlayerTrun += PlayUpAnimation;
    }

    void OnDisable()
    {
        BattleManager.OnPlayerTrun -= PlayUpAnimation;
    }

    public void PlayDownAnimation()
    {
        animator.Play("MainUIDown");
    }

    public void PlayUpAnimation()
    {
        animator.Play("MainUIUp");
    }
}
