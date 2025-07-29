using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayDownAnimation()
    {
        animator.Play("Down");
    }

    public void PlayUpAnimation()
    {
        animator.Play("UP");
    }
}
