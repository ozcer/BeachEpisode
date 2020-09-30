using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrderer : MonoBehaviour
{
    SpriteRenderer srenderer;

    private void Start()
    {
        srenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        srenderer.sortingOrder = -(int)(transform.position.y * 100);
    }
}
