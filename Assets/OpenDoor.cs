using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]private GameObject[] door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            door[0].transform.position = door[0].transform.position + new Vector3(0, 1f, 0);
            door[1].transform.position = door[1].transform.position + new Vector3(0, -1f, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            door[0].transform.position = door[0].transform.position + new Vector3(0, -1f, 0);
            door[1].transform.position = door[1].transform.position + new Vector3(0, 1f, 0);
        }
    }
}
