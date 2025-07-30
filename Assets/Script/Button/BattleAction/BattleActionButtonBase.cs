using TMPro;
using UnityEngine;

public class BattleActionButtonBase : BattleButtonBase
{
    [SerializeField]
    protected int thisAction = 0;
    GameObject selectArrow;

    protected uiText UIData;
    
    protected TextMeshProUGUI uiText;

    [SerializeField]
    BattleActionSeleter interviewSeleter;

    private void Awake()
    {
        action = thisAction;
        uiText = transform.GetComponentInChildren<TextMeshProUGUI>();
        selectArrow = transform.GetChild(1).gameObject;
    }

    public override void Setup(uiText uiText)
    {
        UIData = uiText;
        this.uiText.text = UIData.UIText;
    }

    public override void SelectThis()
    {
        selectArrow.SetActive(true);
    }

    public override void UnselectedThis()
    {
        selectArrow.SetActive(false);
    }

    public override void Action()
    {
        interviewSeleter.SetUIGroupName(UIData.nextUIGroup);
        interviewSeleter.OnUI();
        seleter.OffUI();
        InputManager.ChangeSelecter(interviewSeleter);
    }
}
