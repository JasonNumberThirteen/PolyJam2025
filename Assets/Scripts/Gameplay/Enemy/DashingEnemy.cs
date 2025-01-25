using UnityEngine;

public class DashingEnemy : Enemy
{
	[SerializeField, Min(0f)] private float distanceToDash = 7f;
	[SerializeField, Min(1f)] private float dashMovementSpeedMultiplier = 7f;
	[SerializeField, Min(0f)] private float dashStopDuration = 2f;

	private bool isDashing;
	private bool isStopped;
	private Vector2 dashPoint;

	private readonly float DISTANCE_TO_DASH_POINT_THRESHOLD = 1f;
	
	protected override void FixedUpdate()
	{
		if(target == null)
		{
			return;
		}

		if(!isDashing && IsCloseToPosition(target.position, distanceToDash))
		{
			isDashing = true;
			dashPoint = target.position;
		}

		if(!isStopped && isDashing && IsCloseToPosition(dashPoint, DISTANCE_TO_DASH_POINT_THRESHOLD))
		{
			isStopped = true;

			Invoke(nameof(StopDashing), dashStopDuration);
		}

		var direction = (GetCurrentPosition() - (Vector2)transform.position).normalized;

		rb2D.velocity = direction*GetMovementSpeed();
	}

	private void StopDashing()
	{
		isStopped = false;
		isDashing = false;
	}

	private float GetMovementSpeed()
	{
		if(isStopped)
		{
			return 0f;
		}
		
		return IsCloseToPosition(target.position, distanceToDash) ? initialMovementSpeed*dashMovementSpeedMultiplier : initialMovementSpeed;
	}

	private Vector2 GetCurrentPosition() => IsCloseToPosition(target.position, distanceToDash) ? dashPoint : target.position;
	private bool IsCloseToPosition(Vector2 position, float distance) => Vector2.Distance(transform.position, position) <= distance;
}