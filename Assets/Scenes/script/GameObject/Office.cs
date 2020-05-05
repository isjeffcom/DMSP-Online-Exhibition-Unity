using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour
{

    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
