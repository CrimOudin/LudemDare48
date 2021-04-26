using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int Value;

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
