using UnityEngine;

public class MoveRadiusRenderer : MonoBehaviour
{
    LineRenderer linerenderer;

    private void Awake()
    {
        linerenderer = GetComponent<LineRenderer>();
        BattleManager.OnEnemyTrun += OnRenderer;
        BattleManager.EndEnemyTrun += OffRenderer;
    }

    private void Start()
    {
        linerenderer.SetPositions(GetMoveRadius());
        OffRenderer();
    }

    //private void Update()
    //{
    //    linerenderer.SetPositions(GetMoveRadius());
    //}

    void OnRenderer()
    {
        linerenderer.enabled = true;
    }

    void OffRenderer()
    {
        linerenderer.enabled = false;
    }

    Vector3[] GetMoveRadius()
    {
        Vector3[] vectors = new Vector3[5];
        vectors[0] = new Vector3(BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x - 0.5f, BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y + 0.5f, 0);
        vectors[1] = new Vector3(BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x + 0.5f, BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y + 0.5f, 0);
        vectors[2] = new Vector3(BattleManager.battlemanager.Center.x + BattleManager.battlemanager.Radius.x + 0.5f, BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y - 0.5f, 0);
        vectors[3] = new Vector3(BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x - 0.5f, BattleManager.battlemanager.Center.y - BattleManager.battlemanager.Radius.y - 0.5f, 0);
        vectors[4] = new Vector3(BattleManager.battlemanager.Center.x - BattleManager.battlemanager.Radius.x - 0.5f, BattleManager.battlemanager.Center.y + BattleManager.battlemanager.Radius.y + 0.5f, 0);

        return vectors;
    }
}
