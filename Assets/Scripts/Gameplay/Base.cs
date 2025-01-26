using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
	public UnityEvent<BaseLevel> baseLevelledUpEvent;
	
	[SerializeField] private List<BaseLevel> baseLevels = new();

	private int currentLevelIndex;
	private int rockResourcePieces;
	private int crystalResourcePieces;

	public BaseLevel GetCurrentBaseLevel() => baseLevels[currentLevelIndex];
	public int GetLeftRockPieces() => GetCurrentBaseLevel().GetNumberOfRequiredRockResourcePieces() - rockResourcePieces;
	public int GetLeftCrystalPieces() => GetCurrentBaseLevel().GetNumberOfRequiredCrystalResourcePieces() - crystalResourcePieces;

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
		}
	}
}