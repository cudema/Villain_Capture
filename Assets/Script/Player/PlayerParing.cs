using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParing : MonoBehaviour
{
    GameObject parringPoint;

    private void Start()
    {
        parringPoint = transform.GetChild(0).gameObject;
        parringPoint.SetActive(false);
    }

    public void OnParring(InputAction.CallbackContext context)
    {
        StartCoroutine(Parring());
    }

    IEnumerator Parring()
    {
        parringPoint.SetActive(true);

        yield return new WaitForSeconds(1f);
        
        parringPoint.SetActive(false);
    }
}
