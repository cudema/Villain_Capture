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

    bool isSkipPrint;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        BattleManager.EndPlayerAction += ResetText;
    }

    void Update()
    {
        if (isPlay && Input.anyKeyDown)
        {
            isSkipPrint = true;
        }
    }

    public void ResetText()
    {
        text.text = "";
    }

    public void Print(EnemyDialogue text)
    {
        isSkipPrint = false;
        StartCoroutine(PrintTextCoroutine(text));
    }

    public IEnumerator PrintTextCoroutine(EnemyDialogue printText)
    {
        if (isPlay || printText == null)
        {
            yield break;
        }

        ResetText();
        string tempText = printText.dialogueText;

        yield return null;

        isPlay = true;
        printText.isUse = true;
        for (int i = 0; i < tempText.Length; i++)
        {
            if (isSkipPrint)
            {
                text.text = tempText;
                yield return null;
                break;
            }

            text.text += tempText[i];

            yield return new WaitForSeconds(printDelay);
        }

        isPlay = false;
        isSkipPrint = false;
        yield return new WaitUntil(() => Input.anyKeyDown);

        DialogueManager.instance.PrintDialogue(printText.nextDialogueGrup);

    }
}
