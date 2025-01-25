using UnityEngine;

public class CounterPanelUI : MonoBehaviour
{
	protected TextUI textUI;

	protected virtual void Awake()
	{
		textUI = GetComponentInChildren<TextUI>();
	}
}