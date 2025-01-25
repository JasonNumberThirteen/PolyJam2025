using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextUI : MonoBehaviour
{
	private TextMeshProUGUI text;
	private Color initialColor;

	public void SetTextEnabled(bool enabled)
	{
		text.enabled = enabled;
	}

	public void SetText(string newText)
	{
		text.text = newText;
	}

	public void SetColor(Color color)
	{
		text.color = color;
	}

	public void RestoreInitialColor()
	{
		text.color = initialColor;
	}

	protected virtual void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
		initialColor = text.color;
	}
}