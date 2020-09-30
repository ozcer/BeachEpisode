using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunFilter : MonoBehaviour
{
    GameManager gm;
    Image image;
    public float startAlpha, endAlpha;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Get();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color oldColor = image.color;
        float a = (endAlpha - startAlpha) * (1f - gm.timeOfDay) + startAlpha;
        image.color = new Color(oldColor.r, oldColor.g, oldColor.b, a / 255f);
    }
}
