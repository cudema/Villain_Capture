using System.Collections;
using UnityEngine;

public class MoveRadiusRenderer : MonoBehaviour
{
    LineRenderer linerenderer;

    private void Awake()
    {
        linerenderer = GetComponent<LineRenderer>();
        linerenderer.SetPositions(GetMoveRadius());
    }

    private void Start()
    {
        linerenderer.SetPositions(GetMoveRadius());
        OffRenderer();
    }

    private void Update()
    {
        //linerenderer.SetPositions(GetMoveRadius());
    }

    void OnEnable()
    {
        BattleManager.OnSetEnemyTrun += OnRenderer;
        BattleManager.EndSetEnemyTrun += OffRenderer;
    }

    void OnDisable()
    {
        BattleManager.OnSetEnemyTrun -= OnRenderer;
        BattleManager.EndSetEnemyTrun -= OffRenderer;
    }

    void OnRenderer()
    {
        StartCoroutine(OnRendererAnimation());
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

    IEnumerator OnRendererAnimation()
    {
        Vector3[] target = GetMoveRadius();
        Vector3[] temp = new Vector3[5];

        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y, 0);
            target[i] -= new Vector3(BattleManager.battlemanager.Center.x, BattleManager.battlemanager.Center.y, 0);
        }
        linerenderer.SetPositions(temp);
        linerenderer.enabled = true;

        while (!BattleManager.battlemanager.isOnEnemy)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] += target[i] * Time.deltaTime / (0.15f * 5.5f);
            }
            linerenderer.SetPositions(temp);
            yield return null;
        }
        linerenderer.SetPositions(GetMoveRadius());
        InputManager.inputManager.ChangeBattleMoveInput();
        yield break;
    }
}
