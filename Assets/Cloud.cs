using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float left, right, time, timeDeviation;
    public bool goLeftFirst = true;
    public float yDeviation;

    private void Start()
    {
        if (goLeftFirst) GoLeft();
        else GoRight();
    }

    public void GoRight()
    {
        LeanTween.moveY(gameObject, transform.position.y + Random.Range(-yDeviation, yDeviation), 0f);
        LeanTween.moveX(gameObject, right, time + Random.Range(-timeDeviation, timeDeviation)).setOnComplete(GoLeft);
    }

    public void GoLeft()
    {
        LeanTween.moveY(gameObject, transform.position.y + Random.Range(-yDeviation, yDeviation), 0f);
        LeanTween.moveX(gameObject, left, time + Random.Range(-timeDeviation, timeDeviation)).setOnComplete(GoRight);
    }
}
