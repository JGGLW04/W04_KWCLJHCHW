using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void SetColorBlack()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }
}
