using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI shotgunText;

    private Color initialShotgunColor;
	
    private void Start()
    {
        PlayerStats.PlayerStatsChanged += UpdateUI;
        Gun.ReloadStatus += Reload;
        initialShotgunColor = shotgunText.color;
    }

    private void Reload(object sender, bool e)
    {
        if (e)
        {
            shotgunText.color = Color.red;
        }
        else
        {
            shotgunText.color = initialShotgunColor;
        }
    }

    private void UpdateUI(object sender, PlayerStats e)
    {
        shotgunText.text = e.shotgunShellAmount.ToString() + "/" + e.initialShotgunShellAmount.ToString();
    }
}