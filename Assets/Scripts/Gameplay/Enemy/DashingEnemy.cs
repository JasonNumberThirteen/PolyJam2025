using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DashingEnemy : Enemy
{
	[SerializeField, Min(0f)] private float distanceToDash = 7f;
	[SerializeField, Min(1f)] private float dashMovementSpeedMultiplier = 7f;
	[SerializeField, Min(0f)] private float dashStopDuration = 2f;
	[SerializeField, Min(0f)] private float additionalDistanceToDashPoint = 3f;

	private bool isDashing;
	private bool isStopped;
	private Vector2 dashPoint;
	private Animator animator;
	private SpriteRenderer spriteRenderer;

	private readonly float DISTANCE_TO_DASH_POINT_THRESHOLD = 2f;
	
	protected override void FixedUpdate()
	{
		if(target == null)
		{
			return;
		}

		if(!isStopped && !isDashing && IsCloseToPosition(target.position, distanceToDash))
		{
			isDashing = true;
			dashPoint = target.position + (target.transform.position - transform.position).normalized*additionalDistanceToDashPoint;

			animator.SetBool("IsDashing", isDashing);
		}

		if(!isStopped && isDashing && IsCloseToPosition(dashPoint, DISTANCE_TO_DASH_POINT_THRESHOLD))
		{
			isStopped = true;

			Invoke(nameof(StopDashing), dashStopDuration);
		}

		var direction = (GetCurrentPosition() - (Vector2)transform.position).normalized;

		if(spriteRenderer != null)
		{
			spriteRenderer.flipX = direction.x > 0;
		}
		
		rb2D.velocity = direction*GetMovementSpeed();
	}

	protected override void Awake()
	{
		base.Awake();

		animator = GetComponent<Animator>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	private void StopDashing()
	{
		isStopped = false;
		isDashing = false;

		animator.SetBool("IsDashing", isDashing);
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
}