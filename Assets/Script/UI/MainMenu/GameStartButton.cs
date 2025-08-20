using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    Toggle toggle;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ScenesManager.instance.LoadTempMain();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toggle.isOn = true;
    }
}
