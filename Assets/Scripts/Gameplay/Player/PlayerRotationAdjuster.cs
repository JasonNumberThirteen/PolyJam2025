using System;
using UnityEngine;

public class PlayerRotationAdjuster : MonoBehaviour
{
    private Plane plane;
    private void Awake()
    {
        plane = new Plane(Vector3.back, Vector3.zero);
    }
    private void Update()
	{
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float enter))
        {
            var worldPosition = ray.GetPoint(enter);
            var direction = worldPosition - transform.position;
            var angle = (float)Math.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        
	}
}