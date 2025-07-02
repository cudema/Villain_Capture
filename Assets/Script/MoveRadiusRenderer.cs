using UnityEngine;

public class MoveRadiusRenderer : MonoBehaviour
{
    LineRenderer linerenderer;
    [SerializeField]
    PlayerContoller player;

    private void Awake()
    {
        linerenderer = GetComponent<LineRenderer>();
        BattleManager.OnEnemyTrun += OnRenderer;
        BattleManager.EndEnemyTrun += OffRenderer;
    }

    private void Start()
    {
        linerenderer.SetPositions(player.GetMoveRadius());
        OffRenderer();
    }

    void OnRenderer()
    {
        linerenderer.enabled = true;
    }

    void OffRenderer()
    {
        linerenderer.enabled = false;
    }
}
