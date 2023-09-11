using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public float delayBetweenDestruction = 1f;
    public List<GameObject> BreakBlocks;
    bool _active = false;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && this.GetComponent<Health>().CurrentHealth <= 0 && !_active)
    //    {
    //        _active = true;
    //        StartDestructionSequence();
    //    }
    //}

    private void FixedUpdate()
    {
        if (this.GetComponent<Health>().CurrentHealth <= 0 && !_active)
        {
            _active = true;
            StartDestructionSequence();
        }
    }
    public void StartDestructionSequence()
    {
        StartCoroutine(DestroyBlocks());
    }

    private IEnumerator DestroyBlocks()
    {
        foreach (GameObject child in BreakBlocks)
        {
            Destroy(child.gameObject);
            yield return new WaitForSeconds(delayBetweenDestruction);
        }
        Destroy(this.gameObject);
    }
}
