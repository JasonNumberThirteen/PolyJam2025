using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage), typeof(Timer))]
public class FadeScreenImageUI : MonoBehaviour
{
	public UnityEvent<bool> fadeWasStartedEvent;
	public UnityEvent<bool> fadeWasFinishedEvent;
	
	[SerializeField] private bool fadeOut = true;
	
	private RawImage rawImage;
	private Timer timer;

	public void SetFadeOut(bool fadeOut)
	{
		this.fadeOut = fadeOut;

		timer.StartTimer();
	}

	private void Awake()
	{
		rawImage = GetComponent<RawImage>();
		timer = GetComponent<Timer>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(true);
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
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
		}
	}

	private void OnTimerStarted()
	{
		fadeWasStartedEvent?.Invoke(fadeOut);

		rawImage.raycastTarget = true;
	}

	private void OnTimerFinished()
	{
		fadeWasFinishedEvent?.Invoke(fadeOut);

		rawImage.raycastTarget = false;
	}

	private void Update()
	{
		if(timer.TimerWasStarted)
		{
			rawImage.color = GetColor();
		}
	}

	private Color GetColor()
	{
		var color = rawImage.color;
		var progress = timer.GetProgress();

		//Debug.Log(progress);

		color.a = fadeOut ? 1 - progress : progress;

		return color;
	}
}