using UnityEngine;

[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(Animator))]
public class ShootingEnemy : Enemy
{
	[SerializeField, Min(0f)] private float distanceToStopMoving = 5f;
	[SerializeField, Min(0.01f)] private float thresholdForMovingAway = 1f;
	[SerializeField] private FlyingProjectile flyingProjectilePrefab;
	[SerializeField] private float projectileSpeed = 10f;

	private Timer timer;
	private Animator animator;
	private SpriteRenderer spriteRenderer;

	public void ResetAnimatorParameter()
	{
		animator.SetBool("IsFiring", false);
	}

	protected override void Awake()
	{
		base.Awake();

		timer = GetComponent<Timer>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		RegisterToListeners(true);
	}

	protected override void FixedUpdate()
	{
		if(target == null)
		{
			return;
		}

		var direction = (target.position - transform.position).normalized;

		spriteRenderer.flipX = direction.x > 0;

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
		animator.SetBool("IsFiring", true);
		
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