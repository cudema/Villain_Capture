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
    }

    void OnEnable()
    {
        BattleManager.EndPlayerAction += ResetText;
    }

    void OnDisable()
    {
        BattleManager.EndPlayerAction -= ResetText;
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

    public IEnumerator PrintItemCoroutine(string text)
    {
        if (isPlay || text == null)
        {
            yield break;
        }
        ResetText();
        yield return null;
        isPlay = true;

        for (int i = 0; i < text.Length; i++)
        {
            if (isSkipPrint)
            {
                this.text.text = text;
                yield return null;
                break;
            }

            this.text.text += text[i];

            yield return new WaitForSeconds(printDelay);
        }
        isPlay = false;
        isSkipPrint = false;
        yield return new WaitUntil(() => Input.anyKeyDown);
        BattleManager.battlemanager.StopAction();
    }

    public IEnumerator PrintItemCoroutine(string text, string nextDialogueID)
    {
        if (isPlay || text == null)
        {
            yield break;
        }
        ResetText();
        yield return null;
        isPlay = true;

        for (int i = 0; i < text.Length; i++)
        {
            if (isSkipPrint)
            {
                this.text.text = text;
                yield return null;
                break;
            }

            this.text.text += text[i];

            yield return new WaitForSeconds(printDelay);
        }
        isPlay = false;
        isSkipPrint = false;
        yield return new WaitUntil(() => Input.anyKeyDown);
        DialogueManager.instance.StartCoroutine(DialogueManager.instance.PrintDialogue(nextDialogueID));
    }

    public IEnumerator PrintTextCoroutine(EnemyDialogue printText)
    {
        if (isPlay || printText == null)
        {
            yield break;
        }

        isSkipPrint = false;

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

        DialogueManager.instance.StartCoroutine(DialogueManager.instance.PrintDialogue(printText.nextDialogueGrup));
    }
}
