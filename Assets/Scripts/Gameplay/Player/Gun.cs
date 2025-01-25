using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static EventHandler<bool> ReloadStatus;
    public static EventHandler<bool> OutOfAmmoStatus;

    Movement movement;
    [SerializeField] float shotgunKnockback = 5f;

    [SerializeField] float shotReload = 2f;
    [SerializeField] float shotReloadTimer = 0f;
    bool hasShot = false;
    bool outOfAmmo = false;
    private void Awake()
    {
        movement = GetComponent<Movement>();
    }
    private void Start()
    {
        InputEvents.ShootAction += Shoot;
        OutOfAmmoStatus += OutOfAmmo;
    }

    private void OutOfAmmo(object sender, bool e)
    {
        outOfAmmo = e;
    }

    private void OnDisable()
    {
        InputEvents.ShootAction -= Shoot;
        OutOfAmmoStatus -= OutOfAmmo;
    }

    private void Shoot(object sender, EventArgs e)
    {
        if (outOfAmmo)
            return;

        if(!hasShot) 
        {
            Plane plane = new Plane(Vector3.back, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 worldPosition = ray.GetPoint(enter);

                Vector2 forceDirection = transform.position - worldPosition;
                movement.SendImpulse(forceDirection.normalized * shotgunKnockback);
                hasShot = true;
                ReloadStatus.Invoke(this, true);
            }
        }
        
    }

    private void Update()
    {
        if (!hasShot)
            return;
        if (outOfAmmo)
            return;

        if (shotReloadTimer < shotReload)
        {
            shotReloadTimer += Time.deltaTime;
        }
        else
        {
            hasShot = false;
            shotReloadTimer = 0;
            ReloadStatus.Invoke(this, false);
        }
    }

}
