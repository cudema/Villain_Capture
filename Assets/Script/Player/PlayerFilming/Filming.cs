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
    float maxZoom;
    [SerializeField]
    float zoomSpeed;
    [SerializeField]
    float focusSpeed;
    [SerializeField]
    float cameraRotationSpeed;
    Slider[] sliders = new Slider[2];
    [SerializeField]
    public float filmingTime;
    int perfactDistance;
    public bool justZoom { get; private set; }
    public int justFocus { get; private set; }
    float focus;
    public float Focus
    {
        get => focus;
        set
        {
            focus = value;
            if (focus > 10)
            {
                focus = -10f;
                return;
            }
            if (focus < -10)
            {
                focus = 10f;
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
            zoom = Mathf.Clamp(value, -5, 5);
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
        sliders = rawImage.transform.GetComponentsInChildren<Slider>();
    }

    void OnEnable()
    {
        BattleManager.EndPlayerAction += OffFilming;
    }

    void OnDisable()
    {
        BattleManager.EndPlayerAction -= OffFilming;
    }

    void Update()
    {
        //Focus += updateFocus;
        float camtemp = zoom * 5 + maxZoom + 25;
        cam.fieldOfView = camtemp;
        float temp = focus * 0.001f + zoom * 0.002f;
        useMaterialData.SetFloat("_Intensity", temp);

        sliders[0].value = (zoom + 5) * 6 / 10;
        sliders[1].value = (focus + 10) * 6 / 20;
        ChackRange();
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
        Zoom -= value.ReadValue<float>() * zoomSpeed/* * Time.deltaTime*/;
    }

    public void OnChangeFocus(InputAction.CallbackContext value)
    {
        Focus += value.ReadValue<float>() * focusSpeed/* * Time.deltaTime*/;
    }

    public void OnChangeRotation(InputAction.CallbackContext value)
    {
        Vector2 temp = value.ReadValue<Vector2>();
        x -= temp.y * cameraRotationSpeed * Time.deltaTime;
        y += temp.x * cameraRotationSpeed * Time.deltaTime;
        cam.transform.localRotation = Quaternion.Euler(x, y, 0);
    }

    void ChackRange()
    {
        if (sliders[0].value == perfactDistance)
        {
            ColorBlock temp = sliders[0].colors;
            temp.disabledColor = Color.green;
            sliders[0].colors = temp;
            justZoom = true;
        }
        else
        {
            ColorBlock temp = sliders[0].colors;
            temp.disabledColor = Color.white;
            sliders[0].colors = temp;
            justZoom = false;
        }

        if (sliders[0].value + sliders[1].value == 6)
        {
            ColorBlock temp = sliders[1].colors;
            temp.disabledColor = Color.green;
            sliders[1].colors = temp;
        }
        else
        {
            ColorBlock temp = sliders[1].colors;
            temp.disabledColor = Color.white;
            sliders[1].colors = temp;
        }
        justFocus = (int)(sliders[0].value + sliders[1].value) - 6;
    }

    public void SetPerfactDistance()
    {
        perfactDistance = UnityEngine.Random.Range(0, 6);
    }
}
