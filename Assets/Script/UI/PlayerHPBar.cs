using TMPro;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    IHealthReporter player;

    RectTransform PhotoBar;
    TextMeshProUGUI text;

    private void Start()
    {
        player = PlayerContoller.instance.health;
        PhotoBar = transform.GetChild(0).GetComponent<RectTransform>();
        text = transform.GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"{player.GetMaxHealth()}/{player.GetMaxHealth()}";
        player.ChangeHealth += ChagePhotoBarUI;
    }

    void ChagePhotoBarUI(float photoGauge)
    {
        PhotoBar.localScale = new Vector3(photoGauge / player.GetMaxHealth(), 1, 1);
        text.text = $"{photoGauge}/{player.GetMaxHealth()}";
    }
}
