using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public List<Sprite> lightSprites = new List<Sprite>();
    public int currentLevel = -1; //-1 is brightest, lightSprites.Count is darkest
    public bool canUpdate = false;

    private Vector2 initial = new Vector2();

    private void Awake()
    {
        initial = GetComponent<SpriteRenderer>().size;    
    }

    public void SetLightLevel(int level)
    {
        currentLevel = level;
        if (currentLevel < 0)
            GetComponent<SpriteRenderer>().sprite = null;
        else
            GetComponent<SpriteRenderer>().sprite = lightSprites[currentLevel];

        GetComponent<SpriteRenderer>().size = initial;
    }

    public void IncreaseDarkness()
    {
        if (currentLevel < lightSprites.Count - 1)
            currentLevel++;

        GetComponent<SpriteRenderer>().sprite = lightSprites[currentLevel];
        GetComponent<SpriteRenderer>().size = initial;
    }

    public void IncreaseLightness()
    {
        if (currentLevel >= 0)
            currentLevel--;

        if (currentLevel < 0)
            GetComponent<SpriteRenderer>().sprite = null;
        else
            GetComponent<SpriteRenderer>().sprite = lightSprites[currentLevel];

        GetComponent<SpriteRenderer>().size = initial;
    }

    private void FixedUpdate()
    {
        if (canUpdate)
            transform.position = WorldManager.Instance.player.transform.position;
    }
}
