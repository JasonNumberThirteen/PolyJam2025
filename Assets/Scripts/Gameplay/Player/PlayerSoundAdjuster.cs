using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerSoundAdjuster : MonoBehaviour
{
	[SerializeField] private AudioSource breathingSoundAudioSource;
	[SerializeField] private AudioSource lowOxygenLevelSoundAudioSource;

	private PlayerStats playerStats;

	private void Awake()
	{
		playerStats = GetComponent<PlayerStats>();

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
			if(playerStats != null)
			{
				playerStats.playerReachedLowOxygenLevelEvent.AddListener(OnPlayerReachedLowOxygenLevel);
				playerStats.playerReachedStableOxygenLevelEvent.AddListener(OnPlayerReachedStableOxygenLevel);
				playerStats.playerDiedEvent.AddListener(OnPlayerDiedEvent);
			}
		}
		else
		{
			if(playerStats != null)
			{
				playerStats.playerReachedLowOxygenLevelEvent.RemoveListener(OnPlayerReachedLowOxygenLevel);
				playerStats.playerReachedStableOxygenLevelEvent.RemoveListener(OnPlayerReachedStableOxygenLevel);
				playerStats.playerDiedEvent.RemoveListener(OnPlayerDiedEvent);
			}
		}
	}

	private void OnPlayerReachedLowOxygenLevel()
	{
		SetVolumeToAudioSourceIfPossible(breathingSoundAudioSource, 0.25f);
		SetVolumeToAudioSourceIfPossible(lowOxygenLevelSoundAudioSource, 0.25f);

		lowOxygenLevelSoundAudioSource.loop = true;

		lowOxygenLevelSoundAudioSource.Play();
	}

	private void OnPlayerReachedStableOxygenLevel()
	{
		SetVolumeToAudioSourceIfPossible(breathingSoundAudioSource, 1f);
		SetVolumeToAudioSourceIfPossible(lowOxygenLevelSoundAudioSource, 0f);

		lowOxygenLevelSoundAudioSource.loop = false;
		
		lowOxygenLevelSoundAudioSource.Stop();
	}

	private void OnPlayerDiedEvent()
	{
		SetVolumeToAudioSourceIfPossible(breathingSoundAudioSource, 0f);
		SetVolumeToAudioSourceIfPossible(lowOxygenLevelSoundAudioSource, 0f);
	}

	private void SetVolumeToAudioSourceIfPossible(AudioSource audioSource, float volume)
	{
		if(audioSource != null)
		{
			audioSource.volume = volume;
		}
	}
}