using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class OxygenLevelBarImageUI : MonoBehaviour
{
	private Image image;
	private PlayerStats playerStats;

	private void Awake()
	{
		image = GetComponent<Image>();
		playerStats = FindAnyObjectByType<PlayerStats>();

		RegisterToCallbacks(true);
	}

	private void OnDestroy()
	{
		RegisterToCallbacks(false);
	}

	private void RegisterToCallbacks(bool register)
	{
		if(register)
		{
			if(playerStats != null)
			{
				PlayerStats.PlayerStatsChanged += OnPlayerStatsChanged;
			}
		}
		else
		{
			if(playerStats != null)
			{
				PlayerStats.PlayerStatsChanged -= OnPlayerStatsChanged;
			}
		}
	}

	private void OnPlayerStatsChanged(object sender, PlayerStats playerStats)
	{
		if(image != null && playerStats.initialHP > 0)
		{
			image.fillAmount = playerStats.HP / playerStats.initialHP;
		}
	}
}