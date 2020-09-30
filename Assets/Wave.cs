using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public Sounder sounder;
    public float top, bottom, deviation, time;

    // Start is called before the first frame update
    void Start()
    {
        HighTide();
    }

    public void HighTide()
    {
        LeanTween.moveY(gameObject, top + Random.Range(-deviation, deviation), time).setOnComplete(LowTide);
    }

    public void LowTide()
    {
        sounder.PlayWave();
        LeanTween.moveY(gameObject, bottom, time * 1.5f).setOnComplete(HighTide);
    }
}
