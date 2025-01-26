using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OptionButtonUI : MonoBehaviour
{
	private Button button;
	private OptionSelectionAudioSource optionSelectionAudioSource;

	private void Awake()
	{
		button = GetComponent<Button>();
		optionSelectionAudioSource = FindAnyObjectByType<OptionSelectionAudioSource>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			button.onClick.AddListener(OnClick);
		}
		else
		{
			button.onClick.RemoveListener(OnClick);
		}
	}

	private void OnClick()
	{
		if(optionSelectionAudioSource != null)
		{
			optionSelectionAudioSource.Play();
		}
	}
}