using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ShootingEnemy : Enemy
{
	[SerializeField, Min(0f)] private float distanceToStopMoving = 5f;
	[SerializeField, Min(0.01f)] private float thresholdForMovingAway = 1f;
	[SerializeField] private FlyingProjectile flyingProjectilePrefab;
	[SerializeField] private float projectileSpeed = 10f;

	private Timer timer;

	protected override void Awake()
	{
		base.Awake();

		timer = GetComponent<Timer>();

		RegisterToListeners(true);
	}

	protected override void FixedUpdate()
	{
		if(target == null)
		{
			return;
		}

		var direction = (target.position - transform.position).normalized;

		if(!IsCloseToPosition(target.position, distanceToStopMoving))
		{
			rb2D.velocity = direction*initialMovementSpeed;
		}
		else if(!timer.TimerWasStarted)
		{
			timer.StartTimer();
		}
		else if(timer.TimerWasStarted)
		{
			rb2D.velocity = IsCloseToPosition(target.position, distanceToStopMoving - thresholdForMovingAway) ? -1*initialMovementSpeed*direction : Vector2.zero;
		}
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.timerStartedEvent.AddListener(OnTimerStarted);
			timer.timerFinishedEvent.AddListener(OnTimerFinished);
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.AddListener(OnTimerFinished);
		}
	}

	private void OnTimerStarted()
	{
		if(flyingProjectilePrefab != null)
		{
			Instantiate(flyingProjectilePrefab, transform.position, Quaternion.identity).Setup(damage, projectileSpeed, (target.position - transform.position).normalized);
		}
	}

	private void OnTimerFinished()
	{
		if(IsCloseToPosition(target.position, distanceToStopMoving))
		{
			timer.StartTimer();
		}
	}
}