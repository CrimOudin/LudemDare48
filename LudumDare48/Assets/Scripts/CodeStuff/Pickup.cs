using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int Value;

    public void SetValue(int level)
    {
        if (level >= 6)
        {
            float rand = UnityEngine.Random.value;
            if (rand > 0.66f)
            {
                Value = 300;
                GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            else if (rand > 0.33f)
            {
                Value = 200;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else if (level >= 3)
        {
            float rand = UnityEngine.Random.value;
            if (rand > 0.33f)
            {
                Value = 200;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    private void PickupObject()
    {
        UiManager.Instance.UpdateMoney(Value);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && WorldManager.Instance.player.Invulnerable == false)
        {
            PickupObject();
        }
    }
}
