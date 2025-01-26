using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
	public UnityEvent<DiggableResourceType, int> diggableResourcePiecesChangedEvent;
	
	private readonly Dictionary<DiggableResourceType, int> numberOfPiecesOfDiggableResourceByType = new();

	public void ClearResources()
	{
		numberOfPiecesOfDiggableResourceByType.Clear();
		AddDiggableResourcePiecesByType(DiggableResourceType.Rock, 0);
		AddDiggableResourcePiecesByType(DiggableResourceType.Crystal, 0);
	}

	public void AddDiggableResourcePiecesByType(DiggableResourceType diggableResourceType, int numberOfPieces)
	{
		if(numberOfPiecesOfDiggableResourceByType.TryGetValue(diggableResourceType, out var _))
		{
			numberOfPiecesOfDiggableResourceByType[diggableResourceType] += numberOfPieces;
		}
		else
		{
			numberOfPiecesOfDiggableResourceByType[diggableResourceType] = numberOfPieces;
		}

		diggableResourcePiecesChangedEvent?.Invoke(diggableResourceType, numberOfPiecesOfDiggableResourceByType[diggableResourceType]);
	}

	public int GetNumberOfPiecesOfType(DiggableResourceType diggableResourceType) => numberOfPiecesOfDiggableResourceByType.TryGetValue(diggableResourceType, out var numberOfPieces) ? numberOfPieces : 0;
}