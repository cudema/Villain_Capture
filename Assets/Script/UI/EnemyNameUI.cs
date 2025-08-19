using TMPro;
using UnityEngine;

public class EnemyNameUI : MonoBehaviour
{
    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        text.text = BattleManager.battlemanager.CurrentEnemy.enemyName;
    }
}
