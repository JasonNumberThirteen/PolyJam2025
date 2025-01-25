using UnityEngine;

public class AmmoCounterPanelUI : CounterPanelUI
{
	[SerializeField] private Color ammoTextReloadingStatusColor = Color.red;

	public void SetAmmoTextColor(bool startedReloading)
	{
		if(textUI == null)
		{
			return;
		}
		
		if(startedReloading)
		{
			textUI.SetColor(ammoTextReloadingStatusColor);
		}
		else
		{
			textUI.RestoreInitialColor();
		}
	}

	public void SetAmmoValue(int currentAmmo, int maxAmmo)
	{
		if(textUI != null)
		{
			textUI.SetText($"{currentAmmo}/{maxAmmo}");
		}
	}
}