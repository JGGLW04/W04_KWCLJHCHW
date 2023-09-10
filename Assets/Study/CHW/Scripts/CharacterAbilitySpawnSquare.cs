using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

public class CharacterAbilitySpawnSquare : CharacterAbility
{
    [SerializeField] GameObject Square;
    [SerializeField] Vector2 SquareOffset;
    private GameObject _spawnedSquare;
    protected bool _spawning;

    [Tooltip("the speed of the character when it's walking and spaning")]
    public float WalkSpeed = 6f;
    // animation parameters
    protected const string _spawningAnimationParameterName = "Spawning";
    protected int _spawningAnimationParameter;

    protected override void Initialization()
    {
        base.Initialization();
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if (_inputManager.SpawnButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        {
            SpawnStart();
        }

        if (_inputManager.SpawnButton.State.CurrentState == MMInput.ButtonStates.ButtonUp && _spawning)
        {
            SpawnStop();
        }
    }

    protected virtual void SpawnStart()
    {
        if ((!AbilityAuthorized)
            || (!_controller.State.IsGrounded)
            || (_movement.CurrentState == CharacterStates.MovementStates.Falling)
            || (_movement.CurrentState == CharacterStates.MovementStates.Jumping)
            || (_movement.CurrentState == CharacterStates.MovementStates.LadderClimbing)
            || (_movement.CurrentState == CharacterStates.MovementStates.Pushing)
            || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
        {
            return;
        }
        if (_characterHorizontalMovement != null)
        {
            _characterHorizontalMovement.MovementSpeed = WalkSpeed;
        }
        if (_movement.CurrentState != CharacterStates.MovementStates.SpawningSquare)
        {
            PlayAbilityStartFeedbacks();
            _spawning = true;
            MMCharacterEvent.Trigger(_character, MMCharacterEventTypes.SpawnSquare, MMCharacterEvent.Moments.Start);
            _movement.ChangeState(CharacterStates.MovementStates.SpawningSquare);
            if (_spawnedSquare != null)
            {
                Destroy(_spawnedSquare);
            }
            if (_character.IsFacingRight)
            {
                SquareOffset = new Vector2(Mathf.Abs(SquareOffset.x), SquareOffset.y);
            }
            else
            {
                SquareOffset = new Vector2(-Mathf.Abs(SquareOffset.x), SquareOffset.y);
            }
            _spawnedSquare = GameObject.Instantiate(Square, this.transform.position + (Vector3)SquareOffset, new(0, 0, 0, 0));
            //_spawnedSquare.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            _spawnedSquare.transform.parent = GameObject.Find("Squares").transform;
          
        }
    }

    protected virtual void SpawnStop()
    {
        if (_movement.CurrentState == CharacterStates.MovementStates.SpawningSquare)
        {
            StopStartFeedbacks();
            PlayAbilityStopFeedbacks();
            MMCharacterEvent.Trigger(_character, MMCharacterEventTypes.SpawnSquare, MMCharacterEvent.Moments.End);
        }
        _movement.ChangeState(CharacterStates.MovementStates.Idle);
        _spawning = false;
    }

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        if (_movement.CurrentState != CharacterStates.MovementStates.SpawningSquare && _startFeedbackIsPlaying)
        {
            StopStartFeedbacks();
        }
        if (_movement.CurrentState != CharacterStates.MovementStates.SpawningSquare && _spawning)
        {
            SpawnStop();
        }
        if (!_controller.State.IsCollidingBelow && _spawning)
        {
            SpawnStop();
        }
    }
    protected override void InitializeAnimatorParameters()
    {
        ;
    }
    public override void UpdateAnimator()
    {
        ;
    }
    public override void ResetAbility()
    {
        base.ResetAbility();
        SpawnStop();
        ;
    }
}