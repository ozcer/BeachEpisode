using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteShift : MonoBehaviour
{
    public float period;
    public List<Sprite> sprites;
    public Image image;
    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprites[i];
        StartCoroutine(Shift());
    }

    IEnumerator Shift()
    {
        yield return new WaitForSeconds(period);
        i += 1;
        if (i == sprites.Count) i = 0;
        image.sprite = sprites[i];
        StartCoroutine(Shift());
    }
}
