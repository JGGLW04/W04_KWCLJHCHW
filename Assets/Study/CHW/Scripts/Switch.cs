using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool Active = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("MagicCube"))
        {
            Active = true;
            this.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("MagicCube"))
        {
            Active = false;
            this.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
