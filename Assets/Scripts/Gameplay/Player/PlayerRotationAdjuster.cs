using System;
using UnityEngine;

public class PlayerRotationAdjuster : MonoBehaviour
{
	private void Update()
	{
		var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		var angle = (float)Math.Atan2(direction.y, direction.x)*Mathf.Rad2Deg - 90f;

		transform.rotation = Quaternion.Euler(0f, 0f, angle);
	}
}