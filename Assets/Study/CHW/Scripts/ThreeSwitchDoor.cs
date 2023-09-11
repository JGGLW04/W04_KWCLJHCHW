using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThreeSwitchDoor : MonoBehaviour
{
    public Vector3 ClosePos;
    public Vector3 OpenPos;
    public float Speed;
    public List<GameObject> Switches;
    public List<bool> SwitchesState;
    public enum SwitchStates { On, Off }
    public SwitchStates CurrentSwitchState { get; set; }
    public SwitchStates InitialState = SwitchStates.Off;

    public void ActiveDoor()
    {
        CheckSwitchesState();
        if (CurrentSwitchState == SwitchStates.On)
        {
            ;
        }
    }

    public void UnActiveDoor()
    {
        CheckSwitchesState();
        if (CurrentSwitchState == SwitchStates.Off)
        {
            ;
        }
    }

    private void CheckSwitchesState()
    {
        for (int i = 0; i < Switches.Count; i++)
        {
            if (Switches[i].GetComponent<Switch>().Active != SwitchesState[i])
            {
                CurrentSwitchState = SwitchStates.Off;
                return;
            }
        }
        CurrentSwitchState = SwitchStates.On;
    }

    public void MoveDoorToClosePos ()
    {
        if (this.transform.position.y > ClosePos.y)
        {
            transform.Translate(new Vector2(0, -Speed * Time.deltaTime), Space.World);
        }
    }
    public void MoveDoorToOpenPos()
    {
        if (this.transform.position.y < OpenPos.y)
        {
            transform.Translate(new Vector2(0, Speed * Time.deltaTime), Space.World);
        }
    }

    protected virtual void FixedUpdate()
    {
        CheckSwitchesState();
        if (CurrentSwitchState == SwitchStates.Off)
        {
            MoveDoorToClosePos();
        }
        else if (CurrentSwitchState == SwitchStates.On)
        {
            MoveDoorToOpenPos();
        }
    }
}
