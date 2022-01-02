using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PendarCameraSettings : MonoBehaviour
{
    public void SetGrayscale(bool set)
    {
        enabled = set;
    }

    [HideInInspector] public Material grayscaleMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(!grayscaleMaterial)
            grayscaleMaterial = Resources.Load<Material>("Pendar/Settings/grayscaleMaterial");
        Graphics.Blit(source, destination, grayscaleMaterial);
    }
}
