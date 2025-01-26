using UnityEngine;

[RequireComponent(typeof(FadingGraphicUI))]
public class WaveCounterTextUI : TextUI
{
	private FadingGraphicUI fadingGraphicUI;

	protected override void Awake()
	{
		base.Awake();

		fadingGraphicUI = GetComponent<FadingGraphicUI>();
	}

	public void DisplayWaveCounter(int wave)
	{
		SetText($"WAVE {wave}");
		fadingGraphicUI.StartFading(true);
	}
}