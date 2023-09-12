using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Lever : MonoBehaviour
{
    public bool ActiveByWeapon = false;
    public enum SwitchStates { On, Off }
    public SwitchStates CurrentSwitchState { get; set; }
    public SwitchStates InitialState = SwitchStates.Off;

    public UnityEvent SwitchOn;
    public UnityEvent SwitchOff;

    private void Start()
    {
        CurrentSwitchState = InitialState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !ActiveByWeapon)
        {
            SetSwitchOn();
        }
        else if (collision.CompareTag("PlayerBullet") && ActiveByWeapon)
        {
            SetSwitchOn();
        }
    }

    public void SetSwitchOn()
    {
        CurrentSwitchState = SwitchStates.On;
        if (SwitchOn != null)
        {
            SwitchOn.Invoke();
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
