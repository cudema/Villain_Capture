using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class PhotoGaugeUI : MonoBehaviour
{
    IHealthReporter enemy;

    RectTransform PhotoBar;

    private void Start()
    {
        enemy = BattleManager.battlemanager.CurrentEnemy;
        PhotoBar = transform.GetChild(0).GetComponent<RectTransform>();
        enemy.ChangeHealth += ChagePhotoBarUI;
    }

    void OnDisable()
    {
        enemy.ChangeHealth -= ChagePhotoBarUI;
    }

    void ChagePhotoBarUI(float photoGauge)
    {
        StartCoroutine(ChageAnimation(photoGauge));
    }

    IEnumerator ChageAnimation(float photoGauge)
    {
        float temp = photoGauge / enemy.GetMaxHealth() - PhotoBar.localScale.x;

        while (PhotoBar.localScale.x < photoGauge / enemy.GetMaxHealth())
        {
            PhotoBar.localScale += new Vector3(temp, 0, 0) * Time.deltaTime / 0.5f;
            yield return null;
        }
        PhotoBar.localScale = new Vector3(photoGauge / enemy.GetMaxHealth(), 1, 1);
        if (PhotoBar.localScale.x >= 1)
        {
            BattleManager.battlemanager.EscapeBattle();
        }
    }
}
