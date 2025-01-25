using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static EventHandler<PlayerStats> PlayerStatsChanged;
    public static EventHandler<bool> AttachedStatus;

    public float HP = 100f;
    public float initialHP = 100f;
    public int initialShotgunShellAmount = 4;
    public int shotgunShellAmount = 4;

    [SerializeField] private float oxygenLossSpeed;
    [SerializeField] private float oxygenGainSpeed;
    bool isLosingOxygen = true;

    private void Awake()
    {
        AttachedStatus += ChangeAttachedStatus;
        Gun.ReloadStatus += GunReload;
    }

    private void GunReload(object sender, bool e)
    {
        if(e)
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
    }

    void LoseOxygen()
    {
        HP -= Time.deltaTime * oxygenLossSpeed;

        if (HP < 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        HP = 0;
        Debug.Log("DEAD");
    }


    
}
