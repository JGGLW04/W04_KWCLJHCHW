using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine.EventSystems;

/// <summary>
/// Handles all GUI effects and changes
/// </summary>
public class CustomGUIManager : MMSingleton<CustomGUIManager>, MMEventListener<LevelNameEvent>, MMEventListener<ControlsModeEvent>
{
	[Header("Bindings")]

	/// the game object that contains the heads up display (avatar, health, points...)
	[Tooltip("the game object that contains the heads up display (avatar, health, points...)")]
	public GameObject[] HUD;
	/// the jetpack bar
	[Tooltip("the jetpack bar")]
	public HealthBubble[] HealthBubbles;
	/// the pause screen game object
	[Tooltip("the pause screen game object")]
	public GameObject PauseScreen;
	/// the time splash gameobject
	[Tooltip("the time splash gameobject")]
	public GameObject TimeSplash;
	/// The mobile buttons
	[Tooltip("The mobile buttons")]
	public CanvasGroup Buttons;
	/// The mobile arrows
	[Tooltip("The mobile arrows")]
	public CanvasGroup Arrows;
	/// The mobile movement joystick
	[Tooltip("The mobile movement joystick")]
	public CanvasGroup Joystick;
	/// the points counter
	[Tooltip("the points counter")]
	public Text PointsText;
	/// the level display
	[Tooltip("the level display")]
	public Text LevelText;

	[Header("Settings")]

	/// the pattern to apply when displaying the score
	[Tooltip("the pattern to apply when displaying the score")]
	public string PointsPattern = "000000";

	protected float _initialJoystickAlpha;
	protected float _initialArrowsAlpha;
	protected float _initialButtonsAlpha;

	/// <summary>
	/// Initialization
	/// </summary>
	protected override void Awake()
	{
		base.Awake();

		if (Joystick != null)
		{
			_initialJoystickAlpha = Joystick.alpha;
		}
		if (Arrows != null)
		{
			_initialArrowsAlpha = Arrows.alpha;
		}
		if (Buttons != null)
		{
			_initialButtonsAlpha = Buttons.alpha;
		}
	}

	/// <summary>
	/// Initialization
	/// </summary>
	protected virtual void Start()
	{
		RefreshPoints();
	}

	/// <summary>
	/// Sets the HUD active or inactive
	/// </summary>
	/// <param name="index">Player index</param>
	/// <param name="state">If set to <c>true</c> turns the HUD active, turns it off otherwise.</param>
	public virtual void SetHUDActive(int index, bool state)
	{
		if (HUD[index] != null)
		{ 
			HUD[index].SetActive(state);
		}
		if (PointsText!= null)
		{ 
			PointsText.enabled = state;
		}
		if (LevelText!= null)
		{ 
			LevelText.enabled = state;
		}
	}

	/// <summary>
	/// Sets the avatar active or inactive
	/// </summary>
	/// <param name="index">Player index</param>
	/// <param name="state">If set to <c>true</c> turns the HUD active, turns it off otherwise.</param>
	public virtual void SetAvatarActive(int index, bool state)
	{
		if (HUD[index] != null)
		{
			HUD[index].SetActive(state);
		}
	}

	public virtual void SetAvartarFocus(int index)
	{
		foreach (var hud in HUD)
		{
			if (hud == null) { return; }

			if (hud == HUD[index])
			{
				HUD[index].transform.Find("AvatarHead").GetComponent<Image>().color = Color.white;
			}
			else
			{
				hud.transform.Find("AvatarHead").GetComponent<Image>().color = Color.gray;
			}
		}
	}

	/// <summary>
	/// Called by the input manager, this method turns controls visible or not depending on what's been chosen
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	/// <param name="movementControl">Movement control.</param>
	public virtual void SetMobileControlsActive(bool state, InputManager.MovementControls movementControl = InputManager.MovementControls.Joystick)
	{
		if (Joystick != null)
		{
			Joystick.gameObject.SetActive(state);
			if (state && movementControl == InputManager.MovementControls.Joystick)
			{
				Joystick.alpha = _initialJoystickAlpha;
			}
			else
			{
				Joystick.alpha = 0;
				Joystick.gameObject.SetActive(false);
			}
		}

		if (Arrows != null)
		{
			Arrows.gameObject.SetActive(state);
			if (state && movementControl == InputManager.MovementControls.Arrows)
			{
				Arrows.alpha = _initialArrowsAlpha;
			}
			else
			{
				Arrows.alpha = 0;
				Arrows.gameObject.SetActive(false);
			}
		}

		if (Buttons != null)
		{
			Buttons.gameObject.SetActive(state);
			if (state)
			{
				Buttons.alpha=_initialButtonsAlpha;
			}
			else
			{
				Buttons.alpha=0;
				Buttons.gameObject.SetActive (false);
			}
		}
	}

	/// <summary>
	/// Sets the pause.
	/// </summary>
	/// <param name="state">If set to <c>true</c>, sets the pause.</param>
	public virtual void SetPause(bool state)
	{
		if (PauseScreen!= null)
		{ 
			PauseScreen.SetActive(state);
			EventSystem.current.sendNavigationEvents = state;
		}
	}

	/// <summary>
	/// Sets the time splash.
	/// </summary>
	/// <param name="state">If set to <c>true</c>, turns the timesplash on.</param>
	public virtual void SetTimeSplash(bool state)
	{
		if (TimeSplash != null)
		{
			TimeSplash.SetActive(state);
		}
	}
	
	/// <summary>
	/// Sets the text to the game manager's points.
	/// </summary>
	public virtual void RefreshPoints()
	{
		if (PointsText!= null)
		{ 
			PointsText.text = GameManager.Instance.Points.ToString(PointsPattern);
		}
	}
	
	public virtual void UpdateHealthBubble(int currentHealth, int maxHealth, string playerCharacterName)
	{
		if (HealthBubbles == null) { return; }
		if(HealthBubbles.Length <= 0) { return; }

		foreach (var healthBubble in HealthBubbles)
		{
			if (healthBubble == null) { continue; }

			if (healthBubble.PlayerCharacterName == playerCharacterName)
			{
				healthBubble.UpdateHeart(currentHealth, maxHealth);
			}
		}
	}
	
	/// <summary>
	/// Sets the level name in the HUD
	/// </summary>
	public virtual void SetLevelName(string name)
	{
		if (LevelText!= null)
		{ 
			LevelText.text=name;
		}
	}

	/// <summary>
	/// When we catch a level name event, we change our level's name in the GUI
	/// </summary>
	/// <param name="levelNameEvent"></param>
	public virtual void OnMMEvent(LevelNameEvent levelNameEvent)
	{
		SetLevelName(levelNameEvent.LevelName);
	}

	public virtual void OnMMEvent(ControlsModeEvent controlsModeEvent)
	{
		SetMobileControlsActive(controlsModeEvent.Status, controlsModeEvent.MovementControl);
	}

	/// <summary>
	/// On enable, we start listening to events
	/// </summary>
	protected virtual void OnEnable()
	{
		this.MMEventStartListening<LevelNameEvent>();
		this.MMEventStartListening<ControlsModeEvent>();
	}

	/// <summary>
	/// On disable, we stop listening to events
	/// </summary>
	protected virtual void OnDisable()
	{
		this.MMEventStopListening<LevelNameEvent>();
		this.MMEventStopListening<ControlsModeEvent>();
	}
}