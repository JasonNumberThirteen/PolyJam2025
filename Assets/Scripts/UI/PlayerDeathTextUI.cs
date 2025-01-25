public class PlayerDeathTextUI : TextUI
{
	private PlayerStats playerStats;
	private FadeScreenImageUI fadeScreenImageUI;

	protected override void Awake()
	{
		base.Awake();
		
		playerStats = FindAnyObjectByType<PlayerStats>();
		fadeScreenImageUI = FindAnyObjectByType<FadeScreenImageUI>();

		RegisterToCallbacks(true);
	}

	private void Start()
	{
		SetTextEnabled(false);
	}

	private void RegisterToCallbacks(bool register)
	{
		if(register)
		{
			if(playerStats != null)
			{
				playerStats.playerDiedEvent.AddListener(OnPlayerDied);
			}
		}
		else
		{
			if(playerStats != null)
			{
				playerStats.playerDiedEvent.RemoveListener(OnPlayerDied);
			}
		}
	}

	private void OnPlayerDied()
	{
		SetTextEnabled(true);
		Invoke(nameof(FadeIn), 1f);
	}

	private void FadeIn()
	{
		if(fadeScreenImageUI != null)
		{
			fadeScreenImageUI.StartFading(false);
		}
	}
}