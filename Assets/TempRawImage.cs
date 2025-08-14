using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TempRawImage : MonoBehaviour
{
    [SerializeField]
    Image fade;
    public void StartFilming()
    {
        BattleManager.battlemanager.StartCoroutine(BattleManager.battlemanager.ActionTimer(PlayerContoller.instance.filming.filmingTime));
        InputManager.inputManager.ChangeBattlePhotoInput();
    }

    public void OnFadeIn()
    {
        fade.color -= new Color(1, 1, 1, 0);
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void OnFadeOut()
    {
        fade.color -= new Color(1, 1, 1, 0);
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    public void EndPlayerAction()
    {
        BattleManager.battlemanager.isEndPlayerAction = true;
        PlayerContoller.instance.OnAttack();
    }

    public void OnFilming()
    {
        StartCoroutine(Filming());
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

    IEnumerator Filming()
    {
        fade.color = Color.white;
        while (fade.color.a > 0)
        {
            fade.color -= new Color(0, 0, 0, 1) * Time.deltaTime / 0.2f;
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);

        PlayerContoller.instance.filming.OffFilming();
    }
}
