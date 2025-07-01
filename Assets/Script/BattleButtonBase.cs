using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleButtonBase : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    protected TrunManager trunmanager;
    [SerializeField]
    BattleMainSeleter seleter;
    [SerializeField]
    BattleAction action;

    [Header("»ö")]
    [SerializeField]
    Color baseColor;
    [SerializeField]
    Color selectColor;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        seleter.ChangeBattleAction(action);
    }

    public void SelectThis()
    {
        image.color = selectColor;
    }

    public void UnselectedThis()
    {
        image.color = baseColor;
    }

    public virtual void Action()
    {
        Debug.Log("»§");
    }
}
