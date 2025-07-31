using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class Filming : MonoBehaviour
{
    [SerializeField]
    RawImage rawImage;
    [SerializeField]
    Material material;
    [SerializeField]
    Camera cam;
    Material useMaterialData;
    [SerializeField]
    float zoomSpeed;
    [SerializeField]
    float focusSpeed;
    [SerializeField]
    float cameraRotationSpeed;

    float focus;
    public float Focus
    {
        get => focus;
        set
        {
            focus = Mathf.Clamp(value, -10, 10);
            if (focus == 10)
            {
                focus = -9.9f;
            }
            if (focus == -10)
            {
                focus = 9.9f;
            }
        }
    }
    float updateFocus;
    float zoom;
    public float Zoom
    {
        get => zoom;
        set
        {
            zoom = Mathf.Clamp(value, -10, 10);
        }
    }

    float rotationX = 0;
    float x
    {
        get => rotationX;
        set
        {
            rotationX = Mathf.Clamp(value, -30, 30);
        }
    }
    float rotationY = 90;
    float y
    {
        get => rotationY;
        set
        {
            rotationY = Mathf.Clamp(value, 60, 120);
        }
    }

    void Awake()
    {
        useMaterialData = new Material(material);
        rawImage.material = useMaterialData;
    }

    void Update()
    {
        Focus += updateFocus;
        float camtemp = zoom * 5 + 60;
        cam.fieldOfView = camtemp;
        float temp = focus * 0.001f + zoom * 0.001f;
        useMaterialData.SetFloat("_Intensity", temp);
    }

    public void OnFilming()
    {
        rawImage.gameObject.SetActive(true);
    }

    public void OffFilming()
    {
        rawImage.gameObject.SetActive(false);
    }

    public void OnChangeZoom(InputAction.CallbackContext value)
    {
        Zoom -= value.ReadValue<float>() * zoomSpeed * Time.deltaTime;
    }

    public void OnChangeFocus(InputAction.CallbackContext value)
    {
        updateFocus = value.ReadValue<float>() * focusSpeed * Time.deltaTime;
    }

    public void OnChangeRotation(InputAction.CallbackContext value)
    {
        Vector2 temp = value.ReadValue<Vector2>();
        x -= temp.y * cameraRotationSpeed * Time.deltaTime;
        y += temp.x * cameraRotationSpeed * Time.deltaTime;
        cam.transform.localRotation = Quaternion.Euler(x, y, 0);
    }
}
