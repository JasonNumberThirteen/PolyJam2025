using System;
using UnityEngine;

public class AmmoCounterPanelUI : CounterPanelUI
{
	[SerializeField] private GameObject ammoIconUI;

	private PlayerStats playerStats;
	
	protected override void Awake()
	{
		playerStats = FindAnyObjectByType<PlayerStats>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(playerStats != null)
			{
				Gun.PlayerShotBullet += FiredBullet;
				Gun.ReloadStatus += OnReloadFinished;
				//playerStats.playerFiredBulletEvent.AddListener(OnPlayerFiredBullet);
				playerStats.playerReceivedBulletsEvent.AddListener(OnPlayerReceivedBullets);
			}
		}
		else
		{
			if(playerStats != null)
			{
                Gun.PlayerShotBullet -= FiredBullet;
                Gun.ReloadStatus -= OnReloadFinished;
                //playerStats.playerFiredBulletEvent.RemoveListener(OnPlayerFiredBullet);
                playerStats.playerReceivedBulletsEvent.RemoveListener(OnPlayerReceivedBullets);
			}
		}
	}

    private void OnReloadFinished(object sender, bool e)
    {
		if (e)
			return;

        var ammoIcons = GetComponentsInChildren<AmmoIconUI>();

        if (ammoIcons != null && ammoIcons.Length > 0)
        {
            ammoIcons[0].SetReductionState();
        }
    }

    private void FiredBullet(object sender, EventArgs e)
    {
        var ammoIcons = GetComponentsInChildren<AmmoIconUI>();

        if (ammoIcons != null && ammoIcons.Length > 0)
        {
            ammoIcons[0].SetGrayedOutState();
        }
    }

    private void OnPlayerFiredBullet()
	{
		var ammoIcons = GetComponentsInChildren<AmmoIconUI>();

		if(ammoIcons != null && ammoIcons.Length > 0)
		{
			ammoIcons[0].SetReductionState();
		}
	}

	private void OnPlayerReceivedBullets(int numberOfBullets)
	{
		var ammoIcons = GetComponentsInChildren<AmmoIconUI>();
		
		if(ammoIcons != null && ammoIcons.Length < numberOfBullets)
		{
			for (int i = 0; i < numberOfBullets - ammoIcons.Length; ++i)
			{
				Instantiate(ammoIconUI, gameObject.transform);
			}
		}
	}
}