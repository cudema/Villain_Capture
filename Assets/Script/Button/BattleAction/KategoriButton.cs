using UnityEngine;
using UnityEngine.EventSystems;

public class KategoriButton : MonoBehaviour //IPointerClickHandler
{
    [SerializeField]
    BattleItemSeleter seleter;

    [SerializeField]
    int kategori;
    [SerializeField]
    int page;

    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     seleter.OpenButton(kategori, page);
    // }

    public void OnKategori()
    {
        seleter.OpenButton(kategori, page);
    }
}
