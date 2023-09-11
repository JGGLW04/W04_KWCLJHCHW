using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class MagicCube : MonoBehaviour
{
    public List<CorgiController> collidingControllers = new List<CorgiController>();

    public void DetachCharacters()
    {
        // Detach all colliding controllers
        foreach (var controller in collidingControllers)
        {
            controller.DetachFromMovingPlatform();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("MC: OnCollisionEnter2D");
        var controller = other.gameObject.GetComponent<CorgiController>();
        if (controller != null)
        {
            collidingControllers.Add(controller);
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("MC: OnCollisionExit2D");
        var controller = other.gameObject.GetComponent<CorgiController>();
        if (controller != null)
        {
            collidingControllers.Remove(controller);
        }
    }
}
