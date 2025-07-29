using UnityEngine;

public abstract class TurnBase<T> where T : MonoBehaviour
{
    public abstract void OnEnter();
    public abstract void OnExit();
}
