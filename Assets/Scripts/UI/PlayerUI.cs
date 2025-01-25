using UnityEngine;
using UnityEngine.Events;

public class PlayerUI : MonoBehaviour
{
    public UnityEvent<bool> PlayerReloadStatusChangedEvent;
	
	private AmmoCounterPanelUI ammoCounterPanelUI;

	private void Awake()
	{
		ammoCounterPanelUI = FindAnyObjectByType<AmmoCounterPanelUI>();
	}

    private void Start()
    {
        PlayerStats.PlayerStatsChanged += UpdateUI;
        Gun.ReloadStatus += ChangeReloadStatus;
    }

    private void ChangeReloadStatus(object sender, bool startedReloading)
    {
        if(ammoCounterPanelUI != null)
		{
			ammoCounterPanelUI.SetAmmoTextColor(startedReloading);
		}
    }

    private void UpdateUI(object sender, PlayerStats e)
    {
        if(ammoCounterPanelUI != null)
		{
			ammoCounterPanelUI.SetAmmoValue(e.shotgunShellAmount, e.initialShotgunShellAmount);
		}
    }
}