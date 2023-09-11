using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// A stun zone will stun any character with a CharacterStun ability entering it
	/// </summary>
	public class DamageZone : CorgiMonoBehaviour
	{

		[Header("Stun Zone")]
		// the layers that will be stunned by this object
		[Tooltip("the layers that will be stunned by this object")]
		public LayerMask TargetLayerMask;
		/// whether or not to disable the zone after the stun has happened
		[Tooltip("whether or not to disable the zone after the stun has happened")]
		public bool DisableZoneOnStun = false;
        
		[Header("Auto Disable")]
		/// if this is true, the zone will be disabled after the duration has passed
		[Tooltip("if this is true, the zone will be disabled after the duration has passed")]
		public bool AutoDisable = true;
		/// the duration (in seconds) before the zone auto disables itself
		[Tooltip("the duration (in seconds) before the zone auto disables itself")]
		[MMCondition("AutoDisable", true)]
		public float AutoDisableDuration = 0.3f;
	
		protected Health _health;
		/*protected Character _character;
		protected CharacterStun _characterStun;*/

		/// <summary>
		/// On enable, we start our disable countdown if necessary
		/// </summary>
		protected void OnEnable()
		{
			if (AutoDisable)
			{
				StartCoroutine(AutoDisableCo());
			}
		}

		/// <summary>
		/// A coroutine used to disable the zone after a certain duration
		/// </summary>
		/// <returns></returns>
		protected virtual IEnumerator AutoDisableCo()
		{
			yield return MMCoroutine.WaitFor(AutoDisableDuration);
			this.gameObject.SetActive(false);
		}

		/// <summary>
		/// When colliding with a gameobject, we make sure it's a target, and if yes, we stun it
		/// </summary>
		/// <param name="collider"></param>
		protected virtual void Colliding(GameObject collider)
		{
			if (!MMLayers.LayerInLayerMask(collider.layer, TargetLayerMask))
			{
				return;
			}
			
			_health = collider.GetComponent<Health>();
			if (_health != null)
			{
				_health.Damage(1f, this.gameObject, .2f, .5f, Vector2.zero);
			}
            
			if (DisableZoneOnStun)
			{
				this.gameObject.SetActive(false);
			}
		}
        
		/// <summary>
		/// When a collision with the player is triggered, we give damage to the player and knock it back
		/// </summary>
		/// <param name="collider">what's colliding with the object.</param>
		public virtual void OnTriggerStay2D(Collider2D collider)
		{
			if (collider.gameObject.CompareTag("Breakable"))
			{
				Debug.Log("OnTriggerStay2D " + collider.gameObject.name);
			}
			//Debug.Log("OnTriggerStay2D " + collider.gameObject.name);
			Colliding(collider.gameObject);
		}

		/// <summary>
		/// On trigger enter 2D, we call our colliding endpoint
		/// </summary>
		/// <param name="collider"></param>S
		public virtual void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.gameObject.CompareTag("Breakable"))
			{
				Debug.Log("OnTriggerEnter2D " + collider.gameObject.name);
			}
			//Debug.Log("OnTriggerEnter2D " + collider.gameObject.name);
			Colliding(collider.gameObject);
		}

		public void OnCollisionEnter2D(Collision2D other)
		{
			//Debug.Log("OnCollisionEnter2D " + other.gameObject.name);
			Colliding(other.gameObject);
		}
		
		public void OnCollisionStay2D(Collision2D other)
		{
			//Debug.Log("OnCollisionStay2D " + other.gameObject.name);
			Colliding(other.gameObject);
		}
	}    
}