using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CandleEffect : MonoBehaviour
{
    Volume volume;
    MultiVignetteVolumeComponent multiVignetteData;
    Vector2 playerPos;
    Camera mainCamera;


    void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out multiVignetteData);
        mainCamera = GameObject.Find("PixelCamera").GetComponent<Camera>();
    }

    void Start()
    {
        playerPos = PlayerContoller.instance.transform.position;
        multiVignetteData.vignette1Center.value = mainCamera.WorldToViewportPoint(playerPos);
        multiVignetteData.vignette2Center.value = mainCamera.WorldToViewportPoint(transform.position);
    }

    void Update()
    {
        playerPos = PlayerContoller.instance.transform.position;
        multiVignetteData.vignette1Center.value = mainCamera.WorldToViewportPoint(playerPos);
        multiVignetteData.vignette2Center.value = mainCamera.WorldToViewportPoint(transform.position);
    }

    public IEnumerator VignetteEffect()
    {
        while (multiVignetteData.vignette1Intensity.value > 0.2)
        {
            multiVignetteData.vignette1Intensity.value -= 0.01f;

            yield return null;
        }

        multiVignetteData.vignette2Intensity.value = 0.2f;
    }

    IEnumerator OffEffect()
    {
        while (multiVignetteData.vignette1Intensity.value < 1)
        {
            multiVignetteData.vignette1Intensity.value += 0.05f;

            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndEffect();
        }
    }

    public void EndEffect()
    {
        StopAllCoroutines();
        StartCoroutine(OffEffect());
    }
}
