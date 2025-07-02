using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Action();
    }

    public virtual void SelectThis()
    {
        image.color = selectColor;
    }

    public virtual void UnselectedThis()
    {
        image.color = baseColor;
    }

    public virtual void Action()
    {
        Debug.Log("»§");
    }
}
