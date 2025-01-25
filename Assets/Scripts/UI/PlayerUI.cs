using UnityEngine;
using UnityEngine.Events;

public class PlayerUI : MonoBehaviour
{
    public UnityEvent<bool> PlayerReloadStatusChangedEvent;
	
	private AmmoPanelUI ammoPanelUI;

	private void Awake()
	{
		ammoPanelUI = FindAnyObjectByType<AmmoPanelUI>();
	}

    private void Start()
    {
        PlayerStats.PlayerStatsChanged += UpdateUI;
        Gun.ReloadStatus += ChangeReloadStatus;
    }

    private void ChangeReloadStatus(object sender, bool startedReloading)
    {
        if(ammoPanelUI != null)
		{
			ammoPanelUI.SetAmmoTextColor(startedReloading);
		}
    }

    private void UpdateUI(object sender, PlayerStats e)
    {
        if(ammoPanelUI != null)
		{
			ammoPanelUI.SetAmmoValue(e.shotgunShellAmount, e.initialShotgunShellAmount);
		}
    }
}