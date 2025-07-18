using System.Collections;
using TMPro;
using UnityEngine;


public class PrintDialogue : MonoBehaviour
{
    [SerializeField]
    float printDelay;

    TextMeshProUGUI text;
    [SerializeField]
    bool isPlay = false;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void ResetText()
    {
        text.text = "";
    }

    public IEnumerator PrintTextCoroutine(string printText)
    {
        if (isPlay)
        {
            yield break;
        }

        isPlay = true;
        ResetText();

        for (int i = 0; i < printText.Length; i++)
        {
            text.text += printText[i];

            yield return new WaitForSeconds(printDelay);
        }

        Debug.Log(0);
        isPlay = false;
    }
}
