using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    [SerializeField] Vector3 Rotation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Turn());
    }

    IEnumerator Turn()
    {
        yield return new WaitForSeconds(0.2f);
        transform.rotation = Quaternion.Euler(Rotation);
    }
}
