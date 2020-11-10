using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SykboxMod : MonoBehaviour
{
    // Start is called before the first frame update
    public Color colorStart;
    public Color colorEnd;
    public float duration = 1.0F;
    void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(colorStart, colorEnd, lerp));
    }

}
