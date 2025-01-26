using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Timer	))]
public class FadingGraphicUI : MonoBehaviour
{
	private Timer timer;
	private Graphic graphic;
	private bool fadeOut;

	public void StartFading(bool fadeOut)
	{
		this.fadeOut = fadeOut;
		
		Invoke(nameof(Fade), 2f);
	}

	private void Fade()
	{
		timer.StartTimer();
	}
	
	private void Awake()
	{
		timer = GetComponent<Timer>();
		graphic = GetComponent<Graphic>();
	}

	private void Update()
	{
		if(graphic != null && timer.TimerWasStarted)
		{
			graphic.color = GetColor();
		}
	}

	private Color GetColor()
	{
		if(graphic == null)
		{
			return new Color(0, 0, 0, 0);
		}
		
		var color = graphic.color;
		var progress = timer.GetProgress();

		color.a = fadeOut ? 1 - progress : progress;

		return color;
	}
}