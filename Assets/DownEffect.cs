using UnityEngine;

public class DownEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem ps;

    void Awake()
    {
        ps.Stop();
    }

    public void OnDownEffect()
    {
        ps.Play();
    }
}
