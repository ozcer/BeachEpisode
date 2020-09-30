using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhettoShadow : MonoBehaviour
{
    public GameManager gm;
    public float minScale = 1f;
    public float maxScale = 2f;
    private void Start()
    {
        gm = GameManager.Get();
    }

    private void Update()
    {
        float newX = minScale + (1f - gm.timeOfDay) * (maxScale - minScale);
        transform.localScale = new Vector2(newX, transform.localScale.y);
    }
}
