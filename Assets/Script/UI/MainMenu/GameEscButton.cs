using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEscButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    Toggle toggle;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toggle.isOn = true;
    }
}
