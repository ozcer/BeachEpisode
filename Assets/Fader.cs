using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Image image;
    public float rate = 0.001f;
    public float maxAlpha = 1f;
    public float minAlpha = 0f;
    public bool startMinAlpha = true;
    bool increasing;

    public CanvasGroup cg;

    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        if (startMinAlpha)
        {
            cg.alpha = minAlpha;
            increasing = false;
        }
        else
        {
            cg.alpha = maxAlpha;
            increasing = true;
            FadeOut();
        }
    }

    private void Update()
    {
        if (increasing && cg.alpha < maxAlpha) cg.alpha += rate;
        else if (!increasing && cg.alpha > minAlpha) cg.alpha -= rate;
    }

    public void FadeIn()
    {
        increasing = true;
    }

    public void FadeOut()
    {
        increasing = false;
    }
}
