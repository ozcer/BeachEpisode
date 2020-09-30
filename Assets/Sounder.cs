using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounder : MonoBehaviour
{
    public List<AudioSource> waves;
    public List<AudioSource> dings;
    public AudioSource money;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWave()
    {
        int i = Random.Range(0, waves.Count);
        waves[i].Play();
    }

    public void PlayDing()
    {
        int i = Random.Range(0, dings.Count);
        dings[i].Play();
    }

    public void PlayMoney()
    {
        money.Play();
    }
}
