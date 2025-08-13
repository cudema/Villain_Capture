using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TempRawImage : MonoBehaviour
{
    [SerializeField]
    Filming filming;
    [SerializeField]
    Image fade;
    public void StartFilming()
    {
        InputManager.inputManager.ChangeBattlePhotoInput();
        StartCoroutine(BattleManager.battlemanager.ActionTimer(filming.filmingTime));
    }

    public void OnFadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void OnFadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    public void EndPlayerAction()
    {
        BattleManager.battlemanager.isEndPlayerAction = true;
    }

    IEnumerator FadeIn()
    {
        while (fade.color.a > 0)
        {
            fade.color -= new Color(0, 0, 0, 1) * Time.deltaTime / 0.06f;
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        while (fade.color.a < 1)
        {
            fade.color += new Color(0, 0, 0, 1) * Time.deltaTime / 0.06f;
            yield return null;
        }
    }
}
