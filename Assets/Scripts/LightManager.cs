using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light[] lights;
    public Material material;
    public Color white;
    public Color red;

    public Vector2 lightIntensity;

    private void Start()
    {
        material.SetColor("_EmissionColor", white);
    }

    public void emergencyMode()
    {
        foreach (Light light in lights)
        {
            light.intensity = lightIntensity.x;
            light.color = Color.red;
        }
        material.SetColor("_EmissionColor", red);
    }

    public void normalMode()
    {
        foreach (Light light in lights)
        {
            light.intensity = lightIntensity.y;
            light.color = Color.white;
        }
        material.SetColor("_EmissionColor", white);
    }
}
