using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DaggerDrop", menuName = "Scriptable Objects/DaggerDrop")]
public class DaggerDrop : PatternBase
{
    [Header("¹ßÆÇ")]
    [SerializeField]
    GameObject floor;
    [SerializeField]
    float xPos;
    [SerializeField]
    float yPos;

    GameObject[] floors = new GameObject[4];

    public override void StartPattern()
    {
        if (isChangedFild)
        {
            BattleManager.battlemanager.ChangeFild(center, radius);
        }

        PlayerContoller.instance.ChangePlayMode(state);

        Vector2 fildCenter = BattleManager.battlemanager.Center;
        Vector2 fildRadius = BattleManager.battlemanager.Radius;

        new Vector3(fildCenter.x - (fildRadius.x - xPos), fildCenter.y - (fildRadius.y - yPos), enemy.transform.position.z);

        floors[0] = Instantiate(floor, new Vector3(fildCenter.x - (fildRadius.x - xPos), fildCenter.y - (fildRadius.y - yPos), enemy.transform.position.z), Quaternion.identity);
        floors[1] = Instantiate(floor, new Vector3(fildCenter.x - (fildRadius.x - xPos), fildCenter.y + (fildRadius.y - yPos), enemy.transform.position.z), Quaternion.identity);
        floors[2] = Instantiate(floor, new Vector3(fildCenter.x + (fildRadius.x - xPos), fildCenter.y - (fildRadius.y - yPos), enemy.transform.position.z), Quaternion.identity);
        floors[3] = Instantiate(floor, new Vector3(fildCenter.x + (fildRadius.x - xPos), fildCenter.y + (fildRadius.y - yPos), enemy.transform.position.z), Quaternion.identity);

        enemy.StartCoroutine(BingPattern());
    }

    protected override IEnumerator BingPattern()
    {
        return base.BingPattern();
    }

    public override void StopPattern()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            Destroy(floors[i]);
        }
        base.StopPattern();
    }
}
