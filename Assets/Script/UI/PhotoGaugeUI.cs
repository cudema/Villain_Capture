using UnityEngine;

public class PhotoGaugeUI : MonoBehaviour
{
    IHealthReporter enemy;

    RectTransform PhotoBar;

    private void Start()
    {
        enemy = BattleManager.CurrentEnemy;
        PhotoBar = transform.GetChild(0).GetComponent<RectTransform>();
        enemy.ChangeHealth += ChagePhotoBarUI;
    }

    void ChagePhotoBarUI(float photoGauge)
    {
        PhotoBar.localScale = new Vector3(photoGauge / enemy.GetMaxHealth(), 1, 1);
    }
}
