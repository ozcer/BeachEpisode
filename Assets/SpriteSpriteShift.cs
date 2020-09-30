using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpriteShift : MonoBehaviour
{
    public float period;
    public List<Sprite> sprites;
    public List<Sprite> ducks;
    public List<Sprite> chosenSprites;
    public SpriteRenderer srender;
    public int i = 0;
    public bool duckForm = false;

    // Start is called before the first frame update
    void Start()
    {
        srender = GetComponent<SpriteRenderer>();
        if (duckForm) chosenSprites = ducks;
        else chosenSprites = sprites;
        srender.sprite = chosenSprites[i];
        StartCoroutine(Shift());
    }

    IEnumerator Shift()
    {
        yield return new WaitForSeconds(period);
        i += 1;
        if (i == sprites.Count) i = 0;
        srender.sprite = chosenSprites[i];
        StartCoroutine(Shift());
    }
}
