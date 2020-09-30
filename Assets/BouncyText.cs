using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BouncyText : MonoBehaviour
{
    public Vector2 minScale = new Vector2(1.2f, 1.2f);
    public Vector2 maxScale = new Vector2(0.8f, 0.8f);
    public float duration = 1f;

    private void Start()
    {
        GoBig();
    }

    void GoBig()
    {
        LeanTween.scale(gameObject, maxScale, duration).setOnComplete(GoSmall);
    }

    void GoSmall()
    {
        LeanTween.scale(gameObject, minScale, duration).setOnComplete(GoBig);
    }
}
