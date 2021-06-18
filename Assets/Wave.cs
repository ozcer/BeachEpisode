using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public Sounder sounder;

    [Range(0f, 1f)]
    // Height of height and low tides relative to wave height
    public float top, bottom;
    
    public float time;
    private float height;
    private RectTransform rt;
    
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        height = rt.sizeDelta.y;
        print("wave heiight is " + height);
        
        HighTide();
    }

    private void HighTide()
    {
        LeanTween.moveY(rt, -height + (top * height), time).setOnComplete(LowTide);
    }

    private void LowTide()
    {
        sounder.PlayWave();
        print(-height);
        LeanTween.moveY(rt, bottom * -height, time * 1.5f).setOnComplete(HighTide);
    }
}
