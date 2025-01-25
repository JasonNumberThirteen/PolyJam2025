using UnityEngine;

public class AmmoPanelUI : MonoBehaviour
{
	[SerializeField] private Color ammoTextReloadingStatusColor = Color.red;
	
	private TextUI ammoTextUI;

	public void SetAmmoTextColor(bool startedReloading)
	{
		if(ammoTextUI == null)
		{
			return;
		}
		
		if(startedReloading)
		{
			ammoTextUI.SetColor(ammoTextReloadingStatusColor);
		}
		else
		{
			ammoTextUI.RestoreInitialColor();
		}
	}

	public void SetAmmoValue(int currentAmmo, int maxAmmo)
	{
		if(ammoTextUI != null)
		{
			ammoTextUI.SetText($"{currentAmmo}/{maxAmmo}");
		}
	}

	private void Awake()
	{
		ammoTextUI = GetComponentInChildren<TextUI>();
	}
}