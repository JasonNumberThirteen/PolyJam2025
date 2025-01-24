using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	public bool TimerWasStarted {get; set;}
	public bool TimerWasFinished {get; set;}
	
	public UnityEvent timerStartedEvent;
	public UnityEvent timerFinishedEvent;

	[SerializeField] private bool startImmediately = true;
	[SerializeField, Min(0.01f)] private float duration = 1f;

	private float currentTime;

	public float GetProgress() => duration > 0f ? currentTime / duration : 0f;

	public void StartTimer()
	{
		SetAsFinished(false);
		timerStartedEvent?.Invoke();
	}

	public void Finish()
	{
		SetAsFinished(true);
		timerFinishedEvent?.Invoke();
	}

	private void Start()
	{
		if(startImmediately)
		{
			StartTimer();
		}
	}

	private void Update()
	{
		if(!TimerWasStarted)
		{
			return;
		}

		if(currentTime < duration)
		{
			currentTime = Mathf.Clamp(currentTime + Time.deltaTime, 0f, duration);
		}
		else if(!TimerWasFinished)
		{
			Finish();
		}
	}

	private void SetAsFinished(bool finished)
	{
		currentTime = finished ? duration : 0f;
		TimerWasStarted = !finished;
		TimerWasFinished = finished;
	}
}