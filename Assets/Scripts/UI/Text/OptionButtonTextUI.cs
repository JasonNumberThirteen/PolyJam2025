using UnityEngine;

public class OptionButtonTextUI : TextUI
{
	[SerializeField] private Color highlightedTextColor;
	[SerializeField] private Color selectedTextColor;
	
	public void SetHighlightedColor()
	{
		SetColor(highlightedTextColor);
	}

	public void SetSelectedColor()
	{
		SetColor(selectedTextColor);
	}

	private void OnEnable()
	{
		RestoreInitialColor();
	}
}