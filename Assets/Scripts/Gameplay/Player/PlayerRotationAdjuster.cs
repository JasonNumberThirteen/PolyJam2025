using System;
using UnityEngine;

public class PlayerRotationAdjuster : MonoBehaviour
{
    private Plane plane;
    private int directionSign = 1;
    private bool isAngleLimited = false;
    private float angle = 0;

    [SerializeField] private float limitAngle = 35;
    private void Awake()
    {
        plane = new Plane(Vector3.back, Vector3.zero);
        Gun.FlipDirection += FlipGun;
        Gun.ReloadStatus += LimitAngle;
    }

    public float GetAngle()
    {
        return angle;
    }

    public bool IsInReloadRange()
    {
        if (angle > limitAngle)
        {
            return false;
        }
        else if (angle < -limitAngle)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void LimitAngle(object sender, bool isReloading)
    {
        if(isReloading)
        {
            isAngleLimited = isReloading;
        }
        
    }

    private void FlipGun(object sender, int direction)
    {
        directionSign = direction;   
    }

    public void UnlockAngle()
    {
        isAngleLimited = false;
    }

    private void Update()
	{
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float enter))
        {
            var worldPosition = ray.GetPoint(enter);
            var direction = worldPosition - transform.position;
            angle = (float)Math.Atan2(direction.y * directionSign, direction.x * directionSign) * Mathf.Rad2Deg;

            if(isAngleLimited)
            {
                if (angle > limitAngle)
                {
                    angle = limitAngle;
                }
                else if (angle < -limitAngle)
                {
                    angle = -limitAngle;
                }
            }
            

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        
	}
}