using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Defines a custom Volume Override component that controls the intensity of the URP Post-processing effect on a Scriptable Renderer Feature.
// For more information about the VolumeComponent API, refer to https://docs.unity3d.com/Packages/com.unity.render-pipelines.core@17.2/api/UnityEngine.Rendering.VolumeComponent.html

// Add the Volume Override to the list of available Volume Override components in the Volume Profile.
[VolumeComponentMenu("Post-processing Custom/MultiVignette")]

// If the related Scriptable Renderer Feature doesn't exist, display a warning about adding it to the renderer.
[VolumeRequiresRendererFeatures(typeof(MultiVignetteRendererFeature))]

// Make the Volume Override active in the Universal Render Pipeline.
[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]

// Create the Volume Override by inheriting from VolumeComponent
public sealed class MultiVignetteVolumeComponent : VolumeComponent, IPostProcessComponent
{
    // Set the name of the volume component in the list in the Volume Profile.
    public MultiVignetteVolumeComponent()
    {
        displayName = "MultiVignette";
    }

    // Create a property to control the intesity of the effect, with a tooltip description.
    // You can set the default value in the project-wide Graphics settings window. For more information, refer to https://docs.unity3d.com/Manual/urp/urp-global-settings.html
    // You can override the value in a local or global volume. For more information, refer to https://docs.unity3d.com/Manual/urp/volumes-landing-page.html
    // To access the value in a script, refer to the VolumeManager API: https://docs.unity3d.com/Packages/com.unity.render-pipelines.core@latest/index.html?subfolder=/api/UnityEngine.Rendering.VolumeManager.html 
    [Tooltip("Enter the description for the property that is shown when hovered")]
    public ClampedFloatParameter intensity = new ClampedFloatParameter(1f, 0f, 1f);

    [Tooltip("첫 번째 Vignette의 중심점입니다. (0,0 = 왼쪽 아래, 1,1 = 오른쪽 위)")]
    public Vector2Parameter vignette1Center = new Vector2Parameter(new Vector2(0.5f, 0.5f));

    [Tooltip("첫 번째 Vignette의 강도입니다. 값이 높을수록 더 어두워집니다.")]
    public ClampedFloatParameter vignette1Intensity = new ClampedFloatParameter(0.5f, 0.0f, 1.0f);

    [Tooltip("첫 번째 Vignette의 가장자리 평활도입니다. 값이 높을수록 가장자리가 부드러워집니다.")]
    public ClampedFloatParameter vignette1Smoothness = new ClampedFloatParameter(0.2f, 0.01f, 1.0f);

    // Vignette 2의 속성 정의
    [Tooltip("두 번째 Vignette의 중심점입니다. (0,0 = 왼쪽 아래, 1,1 = 오른쪽 위)")]
    public Vector2Parameter vignette2Center = new Vector2Parameter(new Vector2(0.5f, 0.5f));

    [Tooltip("두 번째 Vignette의 강도입니다. 값이 높을수록 더 어두워집니다.")]
    public ClampedFloatParameter vignette2Intensity = new ClampedFloatParameter(0.5f, 0.0f, 1.0f);

    [Tooltip("두 번째 Vignette의 가장자리 평활도입니다. 값이 높을수록 가장자리가 부드러워집니다.")]
    public ClampedFloatParameter vignette2Smoothness = new ClampedFloatParameter(0.2f, 0.01f, 1.0f);

    // Optional: Implement the IsActive() method of the IPostProcessComponent interface, and get the intensity value.
    public bool IsActive()
    {
        return intensity.GetValue<float>() > 0.0f;
    }
}
