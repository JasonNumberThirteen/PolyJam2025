using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
	[SerializeField] private Button startGameButtonUI;
	[SerializeField] private Button creditsButtonUI;
	[SerializeField] private Button exitButtonUI;
	[SerializeField] private Button backFromCreditsButtonUI;
	[SerializeField] private GameObject mainMenuPanelUIGO;
	[SerializeField] private GameObject creditsPanelUIGO;

	private FadeScreenImageUI fadeScreenImageUI;

	private readonly string GAMEPLAY_SCENE_NAME = "Gameplay";

	private void Awake()
	{
		Cursor.visible = true;
		fadeScreenImageUI = FindObjectOfType<FadeScreenImageUI>();
		
		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		RegisterToButtonListener(startGameButtonUI, OnStartGameButtonClick, register);
		RegisterToButtonListener(creditsButtonUI, OnCreditsButtonClick, register);
		RegisterToButtonListener(exitButtonUI, OnExitButtonClick, register);
		RegisterToButtonListener(backFromCreditsButtonUI, OnBackFromCreditsButtonClick, register);

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

	private void RegisterToButtonListener(Button button, UnityAction onClick, bool register)
	{
		if(button == null)
		{
			return;
		}

		if(register)
		{
			button.onClick.AddListener(onClick);
		}
		else
		{
			button.onClick.RemoveListener(onClick);
		}
	}

	private void OnStartGameButtonClick()
	{
		if(fadeScreenImageUI != null)
		{
			fadeScreenImageUI.StartFading(false);
		}
	}

	private void OnCreditsButtonClick()
	{
		SetPanelUIsActive(false);
	}

	private void OnExitButtonClick()
	{
		Application.Quit();
	}

	private void OnBackFromCreditsButtonClick()
	{
		SetPanelUIsActive(true);
	}

	private void SetPanelUIsActive(bool mainMenuPanelUIActive)
	{
		SetGOActiveIfPossible(mainMenuPanelUIGO, mainMenuPanelUIActive);
		SetGOActiveIfPossible(creditsPanelUIGO, !mainMenuPanelUIActive);
	}

	private void SetGOActiveIfPossible(GameObject go, bool active)
	{
		if(go != null)
		{
			go.SetActive(active);
		}
	}

	private void OnFadeWasFinished(bool fadeOut)
	{
		if(!fadeOut)
		{
			SceneManager.LoadScene(GAMEPLAY_SCENE_NAME);
		}
	}
}