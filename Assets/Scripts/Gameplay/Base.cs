using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Base : MonoBehaviour
{
	public UnityEvent<BaseLevel> baseLevelledUpEvent;
	
	[SerializeField] private List<BaseLevel> baseLevels = new();
	[SerializeField] private AudioClip baseResourcesDeliverySound;
	[SerializeField] private AudioClip baseLevelUpSound;

	private int currentLevelIndex;
	private int rockResourcePieces;
	private int crystalResourcePieces;
	private AudioSource audioSource;

	public BaseLevel GetCurrentBaseLevel() => baseLevels[currentLevelIndex];
	public int GetLeftRockPieces() => currentLevelIndex < baseLevels.Count ? GetCurrentBaseLevel().GetNumberOfRequiredRockResourcePieces() - rockResourcePieces : 0;
	public int GetLeftCrystalPieces() => currentLevelIndex < baseLevels.Count ? GetCurrentBaseLevel().GetNumberOfRequiredCrystalResourcePieces() - crystalResourcePieces : 0;

	public void DeliverResources(PlayerInventory playerInventory)
	{
		rockResourcePieces += playerInventory.GetNumberOfPiecesOfType(DiggableResourceType.Rock);
		crystalResourcePieces += playerInventory.GetNumberOfPiecesOfType(DiggableResourceType.Crystal);

		if(currentLevelIndex < baseLevels.Count)
		{
			var nextBaseLevel = GetCurrentBaseLevel();

			if(nextBaseLevel.CanAdvanceToThisLevel(rockResourcePieces, crystalResourcePieces))
			{
				++currentLevelIndex;
				rockResourcePieces -= nextBaseLevel.GetNumberOfRequiredRockResourcePieces();
				crystalResourcePieces -= nextBaseLevel.GetNumberOfRequiredCrystalResourcePieces();

				audioSource.PlayOneShot(baseLevelUpSound);
				baseLevelledUpEvent?.Invoke(nextBaseLevel);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.TryGetComponent(out PlayerInventory playerInventory))
		{
			DeliverResources(playerInventory);
			playerInventory.ClearResources();
			audioSource.PlayOneShot(baseResourcesDeliverySound);
		}
	}

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
}