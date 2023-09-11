using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
	public float PositionY;
	public float Speed;
	bool _activate = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
			_activate = true;
		}
    }

	protected virtual void FixedUpdate()
	{
		if (_activate == true && this.transform.position.y > PositionY)
        {
			transform.Translate(new Vector2(0, -Speed * Time.deltaTime), Space.World);
        }
	}
}
