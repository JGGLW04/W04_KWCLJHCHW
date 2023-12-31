﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// A class to handle the display of retro adventure levels in the retro adventure level selector
	/// </summary>
	public class VikingLevel : CorgiMonoBehaviour 
	{
		/// the name of the scene to bind to this element
		[Tooltip("the name of the scene to bind to this element")]
		public string SceneName;
		/// the icon showing whether or not the level is locked
		[Tooltip("the icon showing whether or not the level is locked")]
		public Image LockedIcon;
		/// the button to press to play the level
		[Tooltip("the button to press to play the level")]
		public MMTouchButton PlayButton;

		[Header("Top Image")]

		/// the image showing a preview of the level
		[Tooltip("the image showing a preview of the level")]
		public Image ScenePreview;
		/// the material to apply when the level is off
		[Tooltip("the material to apply when the level is off")]
		public Material OffMaterial;


		/// <summary>
		/// The method to call to go the level specified in the inspector
		/// </summary>
		public virtual void GoToLevel()
		{
			MMSceneLoadingManager.LoadScene(SceneName);
		}

		/// <summary>
		/// On start we initialize our setup
		/// </summary>
		protected virtual void Start()
		{
			InitialSetup ();
		}

		/// <summary>
		/// Sets various elements (stars, locked icon) based on current saved data
		/// </summary>
		protected virtual void InitialSetup()
		{
			foreach (VikingScene scene in VikingProgressManager.Instance.Scenes)
			{
				if (scene.SceneName == SceneName)
				{
					if (scene.LevelUnlocked)
					{
						LockedIcon.gameObject.SetActive (false);
						ScenePreview.material = null;
						ScenePreview.material.SetFloat("_EffectAmount",0);
					}
					else
					{
						LockedIcon.gameObject.SetActive (true);
						ScenePreview.material = OffMaterial;
						ScenePreview.material.SetFloat("_EffectAmount",1);
						PlayButton.DisableButton ();
					}
				}
			}
		}
	}
}