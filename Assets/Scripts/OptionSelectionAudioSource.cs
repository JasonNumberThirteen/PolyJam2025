using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OptionSelectionAudioSource : MonoBehaviour
{
	private AudioSource audioSource;
	
	public void Play()
	{
		audioSource.Play();
	}

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
}