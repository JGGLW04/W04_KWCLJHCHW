using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] int _hp = 3;
    [SerializeField] 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

	protected virtual void FixedUpdate()
	{
		// we send our various states to the animator.		
		//UpdateAnimator();

		//if (_timer < 0)
		//{
		//	_newPosition = new Vector2(0, -FallSpeed * Time.deltaTime);

		//	transform.Translate(_newPosition, Space.World);

		//	if (transform.position.y < _bounds.min.y)
		//	{
		//		DisableFallingPlatform();
		//	}
		//}
	}
}
