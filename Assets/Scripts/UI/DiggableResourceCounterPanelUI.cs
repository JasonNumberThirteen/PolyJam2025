using UnityEngine;

public class DiggableResourceCounterPanelUI : CounterPanelUI
{
	[SerializeField] private DiggableResourceType diggableResourceType;
	
	private PlayerInventory playerInventory;

	protected override void Awake()
	{
		base.Awake();
		
		playerInventory = FindAnyObjectByType<PlayerInventory>();
		
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
			if(playerInventory != null)
			{
				playerInventory.diggableResourcePiecesChangedEvent.AddListener(OnDiggableResourcePiecesChanged);
			}
		}
		else
		{
			if(playerInventory != null)
			{
				playerInventory.diggableResourcePiecesChangedEvent.RemoveListener(OnDiggableResourcePiecesChanged);
			}
		}
	}

	private void OnDiggableResourcePiecesChanged(DiggableResourceType diggableResourceType, int totalNumberOfPieces)
	{
		if(textUI != null && this.diggableResourceType == diggableResourceType)
		{
			textUI.SetText(totalNumberOfPieces.ToString("D3"));
		}
	}
}