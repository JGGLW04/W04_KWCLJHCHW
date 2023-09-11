using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

/// <summary>
/// Add this bar to an object and link it to a bar (possibly the same object the script is on), and you'll be able to resize the bar object based on a current value, located between a min and max value.
/// See the HealthBar.cs script for a use case
/// </summary>
///
[AddComponentMenu("Viking/Tools/GUI/HealthBubble")]
public class HealthBubble : CorgiMonoBehaviour
{
	/*/// 하트버블 기본 단위 
	public enum HeartUnits { One, Half }*/
    
	[MMInspectorGroup("Bindings", true, 10)]
	/// optional - the ID of the player associated to this bar
	[Tooltip("optional - the ID of the player associated to this bar")]
	public string PlayerID;
	// 이 버블과 연결 된 플레이어 캐릭터의 이름
	[Tooltip("이 버블과 연결 된 플레이어 캐릭터의 이름")]
	public string PlayerCharacterName;
	/*/// 버블 프리팹
	[Tooltip("버블 프리팹")]
	public GameObject HeartBubble;*/
    
	/*/// 하트버블 기본 단위
	[Tooltip("하트버블 기본 단위")]
	public HeartUnits HeartUnit = HeartUnits.One;*/

	private int _health;
	private int _numOfHearts;

	public Image[] hearts;
	public Sprite fullHeart;
	//public Sprite halfHeart;
	public Sprite emptyHeart;

	
	protected virtual void Awake()
	{
		Initialization();
	}
	
	protected virtual void Initialization()
	{
		//UpdateHeart();
	}
	
	public void UpdateHeart(int currentHealth, int maxHealth)
	{
		_health = currentHealth;
		_numOfHearts = maxHealth;

		if (_health > _numOfHearts)
		{
			_health = _numOfHearts;
		}
		
		for (var i = 0; i < hearts.Length; i++)
		{
			if (i < _health)
			{
				hearts[i].sprite = fullHeart;
			}
			else
			{
				hearts[i].sprite = emptyHeart;
			}

			if (i < _numOfHearts)
			{
				hearts[i].enabled = true;
			}
			else
			{
				hearts[i].enabled = false;
			}
		}
	}
	
	/// <summary>
	/// A simple method you can call to show the bar (set active true)
	/// </summary>
	public virtual void ShowBubble()
	{
		this.gameObject.SetActive(true);
	}
	
	/// <summary>
	/// Hides (SetActive false) the progress bar object, after an optional delay
	/// </summary>
	/// <param name="delay"></param>
	public virtual void HideBubble(float delay)
	{
		if (delay <= 0)
		{
			this.gameObject.SetActive(false);
		}
		else if (this.gameObject.activeInHierarchy)
		{
			StartCoroutine(HideBubbleCo(delay));
		}
	}

	/// <summary>
	/// An internal coroutine used to handle the disabling of the progress bar after a delay
	/// </summary>
	/// <param name="delay"></param>
	/// <returns></returns>
	protected virtual IEnumerator HideBubbleCo(float delay)
	{
		yield return MMCoroutine.WaitFor(delay);
		this.gameObject.SetActive(false);
	}
}