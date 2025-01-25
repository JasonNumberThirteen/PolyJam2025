using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI airText;
    [SerializeField] TextMeshProUGUI shotgunText;
    Color initialShotgunColor;
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
        if(e.HP >= 100)
        {
            airText.text = e.HP.ToString();
        }
        else
        {
            airText.text = e.HP.ToString("N1");
        }


        shotgunText.text = e.shotgunShellAmount.ToString() + "/" + e.initialShotgunShellAmount.ToString();
        
    }
}
