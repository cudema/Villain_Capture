using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NullPattern", menuName = "Scriptable Objects/NullPattern")]
public class NullPattern : PatternBase
{
    public override void SetPattern()
    {
        base.SetPattern();
    }

    public override void StartPattern()
    {
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        yield return new WaitForSeconds(attackDelay);

        StopPattern();
    }
}
