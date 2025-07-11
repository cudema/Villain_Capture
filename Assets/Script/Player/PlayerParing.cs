using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParing : MonoBehaviour
{
    GameObject parringPoint;
    [SerializeField]
    float parringColldown;
    float currentGameTime = -100;

    private void Start()
    {
        parringPoint = transform.GetChild(0).gameObject;
        parringPoint.SetActive(false);
    }

    public void OnParring(InputAction.CallbackContext context)
    {
        if (Time.time - currentGameTime >  parringColldown)
        {
            currentGameTime = Time.time;
            Debug.Log("parring");
            StartCoroutine(Parring());
        }
    }

    IEnumerator Parring()
    {
        parringPoint.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        
        parringPoint.SetActive(false);
    }
}
