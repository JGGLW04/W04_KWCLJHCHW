using System;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// Add this class to a collider (2D or 3D) and it'll let you trigger things after a duration, like a mine would.
	/// It also comes with options to interrupt or reset the timer on exit. 
	/// </summary>
	public class TimeBomb : CorgiMonoBehaviour
	{
		[MMInspectorGroup("Time Bomb Damage Caused", true, 75)]

		/// the layers that will be damaged by this object
		[Tooltip("the layers that will be damaged by this object")]
		public LayerMask TargetLayerMask;
		/// if this is true, the damage will apply on trigger enter
		[Tooltip("if this is true, the damage will apply on trigger enter")]
		public bool ApplyDamageOnTriggerEnter = true;
		/// if this is true, the damage will apply on trigger stay
		[Tooltip("if this is true, the damage will apply on trigger stay")]
		public bool ApplyDamageOnTriggerStay = true;
		
		/// The min amount of health to remove from the player's health
		[FormerlySerializedAs("DamageCaused")] 
		[Tooltip("The min amount of health to remove from the player's health")]
		public int MinDamageCaused = 1;
		/// The max amount of health to remove from the player's health - has to be above MinDamageCaused, otherwise it'll be set to MinDamageCaused
		[Tooltip("The max amount of health to remove from the player's health, otherwise it'll be set to MinDamageCaused")]
		public int MaxDamageCaused = 0;
		/// the kind of knockback to apply
		[Tooltip("the kind of knockback to apply")]
		public DamageOnTouch.KnockbackStyles Knockback;
		/// The force to apply to the object that gets damaged
		[Tooltip("The force to apply to the object that gets damaged")]
		public Vector2 KnockbackForce = new Vector2(0,0);
		/// The duration of the invincibility frames after the hit (in seconds)
		[Tooltip("The duration of the invincibility frames after the hit (in seconds)")]
		public float InvincibilityDuration = 0.5f;
		/// an existing damage area to activate/handle as the weapon is used
		[Tooltip("an existing damage area to activate/handle as the weapon is used")]
		public DamageOnTouch ExistingDamageArea;
		
		[Header("WarningDuration")] 
		/// the duration of the warning phase, in seconds, betfore the mine triggers
		[Tooltip("the duration of the warning phase, in seconds, betfore the mine triggers")]
		public float WarningDuration = 2f;

		/// a read only display of the current duration before explosion
		[Tooltip("a read only display of the current duration before explosion")]
		[MMReadOnly] 
		public float TimeLeftBeforeTrigger;
        
		[Header("Feedbacks")]
		/// the feedback to play when the warning phase starts
		[Tooltip("the feedback to play when the warning phase starts")]
		public MMFeedbacks OnWarningStartsFeedbacks;
		/// a feedback to play when the warning phase stops
		[Tooltip("a feedback to play when the warning phase stops")] 
		public MMFeedbacks OnWarningStopsFeedbacks;
		/// a feedback to play when the mine triggers
		[Tooltip("a feedback to play when the mine triggers")]
		public MMFeedbacks OnMineTriggerFeedbacks;
        
		protected bool _canExplode = true;
		
		/*protected Collider2D _damageAreaCollider;
		protected BoxCollider2D _boxCollider2D;
		protected GameObject _damageArea;
		protected DamageOnTouch _damageOnTouch;*/
        
		/// <summary>
		/// On Start we initialize our mine
		/// </summary>
		protected virtual void Start()
		{
			Initialization();
		}

		/// <summary>
		/// On init we initialize our feedbacks and duration
		/// </summary>
		public virtual void Initialization()
		{
			//_autoRespawn = this.gameObject.GetComponent<AutoRespawn>();
			/*
			_damageAreaCollider = this.gameObject.GetComponent<Collider2D>();
			*/
			OnWarningStartsFeedbacks?.Initialization();
			OnWarningStopsFeedbacks?.Initialization();
			OnMineTriggerFeedbacks?.Initialization();
			TimeLeftBeforeTrigger = WarningDuration;
		}

		protected void OnEnable()
		{
			TimeLeftBeforeTrigger = WarningDuration;
			_canExplode = true;
			OnWarningStartsFeedbacks?.PlayFeedbacks();
		}

		/// <summary>
		/// Describes what happens when the mine explodes
		/// </summary>
		public virtual void TriggerMine()
		{
			_canExplode = false;
			
			OnMineTriggerFeedbacks?.PlayFeedbacks();
            
			/*// Damage On Explode
			CreateDamageArea();*/
		}

		/// <summary>
		/// On Update if a target is inside the zone, we update our timer
		/// </summary>
		protected virtual void Update()
		{
            TimeLeftBeforeTrigger -= Time.deltaTime;

			if (_canExplode && TimeLeftBeforeTrigger <= 0)
			{
				TriggerMine();
			}
		}
		
		/*/// <summary>
		/// Creates the damage area.
		/// </summary>
		protected virtual void CreateDamageArea()
		{
			if (ExistingDamageArea != null)
			{
				_damageArea = ExistingDamageArea.gameObject;
				_boxCollider2D = _damageArea.GetComponent<BoxCollider2D>();
				_damageOnTouch = ExistingDamageArea;
				_damageAreaCollider = _damageArea.GetComponent<Collider2D>();
				return;
			}
			
			_damageArea = new GameObject();
			_damageArea.name = this.name+"DamageArea";
			_damageArea.transform.position = this.transform.position;
			_damageArea.transform.rotation = this.transform.rotation;
			_damageArea.transform.SetParent(this.transform);


			_damageAreaCollider.isTrigger = true;

			Rigidbody2D rigidBody = _damageArea.AddComponent<Rigidbody2D> ();
			rigidBody.isKinematic = true;

			_damageOnTouch = _damageArea.AddComponent<DamageOnTouch>();
			_damageOnTouch.TargetLayerMask = TargetLayerMask;
			_damageOnTouch.ApplyDamageOnTriggerEnter = ApplyDamageOnTriggerEnter;
			_damageOnTouch.ApplyDamageOnTriggerStay = ApplyDamageOnTriggerStay;
			_damageOnTouch.MinDamageCaused = MinDamageCaused;
			MaxDamageCaused = (MaxDamageCaused <= MinDamageCaused) ? MinDamageCaused : MaxDamageCaused;
			_damageOnTouch.MaxDamageCaused = MaxDamageCaused;
			_damageOnTouch.DamageCausedKnockbackType = Knockback;
			_damageOnTouch.DamageCausedKnockbackForce = KnockbackForce;
			_damageOnTouch.InvincibilityDuration = InvincibilityDuration;
		}*/
	}    
}