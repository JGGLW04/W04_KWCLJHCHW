using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector2 EndPos;
    public bool MoveVertical = false;
    public bool MoveRightOrUp = false;
    public float Speed;
    private bool _active = false;

    public void StartMove()
    {
        _active = true;
    }
    private void FixedUpdate()
    {
        if (_active)
        {
            if (MoveVertical)
            {
                if (MoveRightOrUp)
                {
                    if (this.transform.position.y < EndPos.y)
                        transform.Translate(new Vector2(0, Speed * Time.deltaTime),Space.Self);
                }
                else
                {
                    if (this.transform.position.y > EndPos.y)
                        transform.Translate(new Vector2(0, -Speed * Time.deltaTime), Space.Self);

                }
            }
            else
            {
                if (MoveRightOrUp)
                {
                    if (this.transform.position.x < EndPos.x)
                        transform.Translate(new Vector2(Speed * Time.deltaTime,0), Space.Self);
                }
                else
                {
                    if (this.transform.position.x > EndPos.x)
                        transform.Translate(new Vector2(-Speed * Time.deltaTime, 0), Space.Self);

                }
            }
        }
    }
}
