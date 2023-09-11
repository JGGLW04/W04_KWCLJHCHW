using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPlatform : MonoBehaviour
{
    bool _active = false;
    public Vector3 EndPos;
    public float Speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _active = true;
        }
    }
    private void FixedUpdate()
    {
        if (_active)
        {
             if (this.transform.position.y < EndPos.y)
            {
                transform.Translate(new Vector2(0, Speed * Time.deltaTime), Space.Self);
            }
            else
            {
                this.transform.GetComponent<SpriteRenderer>().color = Color.red;
                _active = false;
            }
        }
    }
}
