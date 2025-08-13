using UnityEngine;

public class BGTile : MonoBehaviour
{
    public float scrollSpeedX = 0.5f;
    public float scrollSpeedY = 0.5f;
    private Renderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // 시간과 속도를 곱해 오프셋 값을 계산
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        
        // 텍스처 오프셋에 적용하여 무한 반복 효과 생성
        myRenderer.material.mainTextureOffset = new Vector2(offsetX, offsetY); 
    }
}