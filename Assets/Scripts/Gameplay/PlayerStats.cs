using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
	public static EventHandler<PlayerStats> PlayerStatsChanged;
	public static EventHandler<bool> AttachedStatus;

	public UnityEvent playerReachedLowOxygenLevelEvent;
	public UnityEvent playerReachedStableOxygenLevelEvent;
	public UnityEvent playerDiedEvent;

	public float HP = 100f;
	public float initialHP = 100f;
	public int initialShotgunShellAmount = 4;
	public int shotgunShellAmount = 4;

	[SerializeField] private float oxygenLossSpeed;
	[SerializeField] private float oxygenGainSpeed;
	[SerializeField, Range(0f, 1f)] private float lowOxygenLevelPercentThreshold = 0.25f;

	private bool isLosingOxygen = true;
	private bool wasDied;
	private OxygenSource oxygenSource;
	private PlayerOxygenLevelType playerOxygenLevelType = PlayerOxygenLevelType.Stable;

	private void Awake()
	{
		AttachedStatus += ChangeAttachedStatus;
		Gun.ReloadStatus += GunReload;

		oxygenSource = FindAnyObjectByType<OxygenSource>();
	}

	private void GunReload(object sender, bool e)
	{
		if(e && (oxygenSource == null || !oxygenSource.PlayerIsAttached()))
		{
			shotgunShellAmount--;
		}

		if(shotgunShellAmount <= 0)
		{
			Gun.OutOfAmmoStatus.Invoke(this, true);
			shotgunShellAmount = 0;
		}
	}

	private void ChangeAttachedStatus(object sender, bool e)
	{
		isLosingOxygen = !e;

		shotgunShellAmount = initialShotgunShellAmount;
		Gun.OutOfAmmoStatus.Invoke(this, false);
	}

	private void Update()
	{
		PlayerStatsChanged.Invoke(this, this);

		if (isLosingOxygen)
			LoseOxygen();
		else
			GainOxygen();
	}

	void GainOxygen()
	{
		HP += Time.deltaTime * oxygenGainSpeed;

		if(HP > initialHP)
		{
			HP = initialHP;
		}
		else if(GetHPPercent() >= lowOxygenLevelPercentThreshold && playerOxygenLevelType != PlayerOxygenLevelType.Stable)
		{
			playerOxygenLevelType = PlayerOxygenLevelType.Stable;

			playerReachedStableOxygenLevelEvent?.Invoke();
		}
	}

	void LoseOxygen()
	{
		HP -= Time.deltaTime * oxygenLossSpeed;

		if(HP < 0)
		{
			Die(); 
		}
		else if(GetHPPercent() < lowOxygenLevelPercentThreshold && playerOxygenLevelType != PlayerOxygenLevelType.Low)
		{
			playerOxygenLevelType = PlayerOxygenLevelType.Low;

			playerReachedLowOxygenLevelEvent?.Invoke();
		}
	}

	void Die()
	{
		if(wasDied)
		{
			return;
		}

		HP = 0;
		wasDied = true;

		playerDiedEvent?.Invoke();
	}

	private float GetHPPercent() => initialHP > 0 ? HP / initialHP : 0f;
}