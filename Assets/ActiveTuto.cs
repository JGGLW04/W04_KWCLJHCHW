using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTuto : MonoBehaviour
{
    [SerializeField] GameObject tutorial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            tutorial.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tutorial.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tutorial.SetActive(false);
        }
    }
}
