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

    public void Print(EnemyDialogue text)
    {
        StartCoroutine(PrintTextCoroutine(text));
    }

    public IEnumerator PrintTextCoroutine(EnemyDialogue printText)
    {
        if (isPlay || printText == null)
        {
            yield break;
        }

        isPlay = true;
        ResetText();
        string tempText = printText.dialogueText;

        for (int i = 0; i < tempText.Length; i++)
        {
            text.text += tempText[i];

            yield return new WaitForSeconds(printDelay);
        }

        isPlay = false;

        yield return new WaitUntil(() => Input.anyKeyDown);

        yield return StartCoroutine(PrintTextCoroutine(TempTextLoad.GetNextDialogue(printText.interviewID, Emotion.무관심)));
    }
}
