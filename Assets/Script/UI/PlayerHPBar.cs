using UnityEngine;

public class HPBar : MonoBehaviour
{
    IHealthReporter player;

    RectTransform PhotoBar;

    private void Start()
    {
        player = PlayerContoller.instance.health;
        PhotoBar = transform.GetChild(0).GetComponent<RectTransform>();
        player.ChangeHealth += ChagePhotoBarUI;
    }

    void ChagePhotoBarUI(float photoGauge)
    {
        PhotoBar.localScale = new Vector3(photoGauge / player.GetMaxHealth(), 1, 1);
    }
}
