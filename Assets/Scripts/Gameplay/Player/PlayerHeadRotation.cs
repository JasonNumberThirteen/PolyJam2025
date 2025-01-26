using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHeadRotation : MonoBehaviour
{
    private Plane plane;
    private int directionSign = 1;
    [SerializeField] private float upperLimitAngle = 35;
    [SerializeField] private float lowerLimitAngle = -10;

    private void Awake()
    {
        plane = new Plane(Vector3.back, Vector3.zero);
        Gun.FlipDirection += FlipGun;
    }

    private void FlipGun(object sender, int direction)
    {
        directionSign = direction;
    }

    private void Update()
    {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float enter))
        {
            var worldPosition = ray.GetPoint(enter);
            var direction = worldPosition - transform.position;
            var angle = (float)Math.Atan2(direction.y * directionSign, direction.x * directionSign) * Mathf.Rad2Deg;

            
                if (angle > upperLimitAngle)
                {
                    angle = upperLimitAngle;
                }
                else if (angle < lowerLimitAngle)
                {
                    angle = lowerLimitAngle;
                }
            


            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }


    }
}
