using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
	private FadeScreenImageUI fadeScreenImageUI;

	private readonly string MAIN_MENU_SCENE_NAME = "MainMenu";

	private void Awake()
	{
		fadeScreenImageUI = FindAnyObjectByType<FadeScreenImageUI>();

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
			if(fadeScreenImageUI != null)
			{
				fadeScreenImageUI.fadeWasFinishedEvent.AddListener(OnFadeWasFinished);
			}
		}
		else
		{
			if(fadeScreenImageUI != null)
			{
				fadeScreenImageUI.fadeWasFinishedEvent.RemoveListener(OnFadeWasFinished);
			}
		}
	}

	private void OnFadeWasFinished(bool fadeOut)
	{
		if(!fadeOut)
		{
			SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
		}
	}
}