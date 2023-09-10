using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

/// <summary>
/// 이 클래스는 유일한 물체를 소환할 수 있도록 합니다.
/// 
/// Animation parameters :
/// - Spawing, boolean, triggered when an object is spawned
/// - SpawningID : int, set to whatever value is set on the spawned object
/// </summary>
[AddComponentMenu("Viking/Character/Abilities/Viking Spawn Unique Object")]
public class VikingAbilitySpawnUniqueObject : CharacterAbility
{
    [Header("Spawn")]
    
    /// 소환할 물체
    [Tooltip("소환할 물체")]
    public GameObject SpawnableObject;
    /// 캐릭터로 부터 물체를 소환할 위치
    [Tooltip("캐릭터로 부터 물체를 소환할 위치")]
    public Vector2 SpawnOffset;

    /// the direction the raycast used to detect grabbable objects will be cast in (if the Character is facing right). Use Vector3.down for Mario2-like grabs from the top, or Vector3.right 
    /// for side grabs for example.
    [Tooltip("the direction the raycast used to detect grabbable objects will be cast in (if the Character is facing right). Use Vector3.down for Mario2-like grabs from the top, or Vector3.right for side grabs for example")]
    public Vector3 RaycastDirection = Vector3.down;
    /// the distance the grab raycast should cover (you'll want it bigger than half your Character's dimensions
    [Tooltip("the distance the grab raycast should cover (you'll want it bigger than half your Character's dimensions")]
    public float RaycastDistance = 1f;
    /// the layer this grab raycast should look for objects on. This should match the layer you put your GrabCarryAndThrowObjects on
    [Tooltip("the layer this grab raycast should look for objects on. This should match the layer you put your GrabCarryAndThrowObjects on")]
    public LayerMask DetectionLayerMask = LayerManager.PlatformsLayerMask | LayerManager.EnemiesLayerMask;
    
    protected VikingGrabCarryAndThrow _vikingGrabCarryAndThrow;
    
    protected GameObject _spawnedObject;
    
    protected Vector2 _raycastOrigin;
    protected Vector3 _actualRaycastDirection;

    // animation parameters
    protected const string _spawningAnimationParameterName = "Spawning";
    protected int _spawningAnimationParameter;
    

    protected override void Initialization()
    {
        base.Initialization();
        _vikingGrabCarryAndThrow = _character?.FindAbility<VikingGrabCarryAndThrow>();
    }

    protected override void HandleInput()
    {
        if (_inputManager.SpawnButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        {
            SpawnAttempt();
        }
    }

    /// <summary>
    /// Raycast를 통해 충돌체를 검출하고, 충돌체가 없을 경우 생성
    /// </summary>
    protected virtual void SpawnAttempt()
    {
        if (!AbilityAuthorized
            || ((_condition.CurrentState != CharacterStates.CharacterConditions.Normal) && (_condition.CurrentState != CharacterStates.CharacterConditions.ControlledMovement)))
        {
            return;
        }
            
        _raycastOrigin = this.transform.position;
        _actualRaycastDirection = RaycastDirection;
        if (!_character.IsFacingRight)
        {
            _actualRaycastDirection = _actualRaycastDirection.MMSetX(-RaycastDirection.x);
        }
        RaycastHit2D hit = MMDebug.RayCast(_raycastOrigin, _actualRaycastDirection, RaycastDistance, DetectionLayerMask, Color.blue, _controller.Parameters.DrawRaycastsGizmos);
        if (!hit)
        {
            Spawn();
        }
    }

    /// <summary>
    /// 물체를 생성
    /// </summary>
    protected virtual void Spawn()
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
        PlayAbilityStartFeedbacks();
        
        MMCharacterEvent.Trigger(_character, MMCharacterEventTypes.SpawnSquare, MMCharacterEvent.Moments.Start);
        
        if (_spawnedObject != null)
        {
            Destroy(_spawnedObject);
            // Carry 모드 취소
            _vikingGrabCarryAndThrow.CarriedObject = null;
            _vikingGrabCarryAndThrow.CarryingID = -1;
            _vikingGrabCarryAndThrow.Carrying = false;
        }
        
        if (_character.IsFacingRight)
        {
            SpawnOffset = new Vector2(Mathf.Abs(SpawnOffset.x), SpawnOffset.y);
        }
        else
        {
            SpawnOffset = new Vector2(-Mathf.Abs(SpawnOffset.x), SpawnOffset.y);
        }
        _spawnedObject = GameObject.Instantiate(SpawnableObject, this.transform.position + (Vector3)SpawnOffset, new(0, 0, 0, 0));

    }
    
    protected override void InitializeAnimatorParameters()
    {
        ;
    }
    public override void UpdateAnimator()
    {
        ;
    }
    
    /// <summary>
    /// 능력 초기화 시, 소환된 물체를 삭제
    /// </summary>
    public override void ResetAbility()
    {
        base.ResetAbility();

        if (_spawnedObject != null)
        {
            Destroy(_spawnedObject);
        }
    }
}