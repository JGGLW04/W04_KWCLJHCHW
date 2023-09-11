using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

public class JumperToNormal : Jumper
{
    protected override void LateUpdate()
    {
		if (_controller != null)
		{
			//_controller.SetVerticalForce(Mathf.Sqrt(2f * JumpPlatformBoost * -_controller.Parameters.Gravity));
			_controller.SetForce(new Vector2(0,0));
			_controller.SetForce(((Vector2)this.transform.up).normalized * JumpPlatformBoost);
			Debug.Log(((Vector2)this.transform.up).normalized * JumpPlatformBoost);
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
