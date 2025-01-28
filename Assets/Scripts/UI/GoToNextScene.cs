using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
	[SerializeField] private string nextSceneName;

	private FadeScreenImageUI fadeScreenImageUI;

	public void GoToScene()
	{
		if(fadeScreenImageUI != null)
		{
			fadeScreenImageUI.StartFading(false);
		}
	}

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
			SceneManager.LoadScene(nextSceneName);
		}
	}
}