using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
public class ChangeGlideVeclocity : MonoBehaviour
{
    public float EnterVerticalForce = 0.0f;
    public float ExitVerticalForce = -2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<VikingGlide>(out VikingGlide glide))
        {
            glide.VerticalForce = EnterVerticalForce;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<VikingGlide>(out VikingGlide glide))
        {
            glide.VerticalForce = ExitVerticalForce;
        }
    }
}
