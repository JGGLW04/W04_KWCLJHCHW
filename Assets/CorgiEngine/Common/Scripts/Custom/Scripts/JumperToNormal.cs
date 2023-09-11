using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

public class JumperToNormal : Jumper
{
	protected override void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("MagicCube"))
        {
			Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
			rb.velocity = Vector2.zero;
			rb.AddForce(((Vector2)this.transform.up).normalized * JumpPlatformBoost, ForceMode2D.Impulse);
			return;
        }
		if  (collider.gameObject.CompareTag("PlayerBullet"))
		{ 
			Projectile bullet = collider.gameObject.GetComponent<Projectile>();
			bullet.Direction = (Vector2)this.transform.up.normalized;
			return;
		}

		_controller = collider.GetComponent<CorgiController>();

		if (_controller == null)
		{
			return;
		}
		else
		{
			if (PreventJumpIfCharacterIsGrounded && (_controller.State.IsGrounded) && (_controller.State.WasGroundedLastFrame))
			{
				_controller = null;
				return;
			}
		}
	}

	protected override void LateUpdate()
    {
		if (_controller != null)
		{
			//_controller.SetVerticalForce(Mathf.Sqrt(2f * JumpPlatformBoost * -_controller.Parameters.Gravity));
			_controller.SetForce(new Vector2(0,0));
			_controller.SetForce(((Vector2)this.transform.up).normalized * JumpPlatformBoost);
			_characterJump = _controller.gameObject.MMGetComponentNoAlloc<Character>()?.FindAbility<CharacterJump>();
			if (_characterJump != null)
			{
				_characterJump.SetCanJumpStop(false);
				_characterJump.SetJumpFlags();
			}
			ActivationFeedback?.PlayFeedbacks();
			_controller = null;
		}
	}
}
