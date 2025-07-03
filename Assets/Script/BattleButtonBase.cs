using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    protected BattleSeleterBase seleter;

    protected int action;

    [Header("»ö")]
    [SerializeField]
    protected Color baseColor;
    [SerializeField]
    protected Color selectColor;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        seleter = transform.GetComponentInParent<BattleSeleterBase>();
    }

    private void Reset()
    {
        baseColor = new Color(1, 1, 1, 1);
        selectColor = new Color(1, 0.51372f, 0.51372f, 1);
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
