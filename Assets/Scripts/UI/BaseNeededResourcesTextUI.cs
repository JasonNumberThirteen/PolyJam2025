public class BaseNeededResourcesTextUI : TextUI
{
	private Base @base;
	private PlayerInventory playerInventory;

	private int rockPieces;
	private int crystalPieces;
	
	protected override void Awake()
	{
		base.Awake();
		
		@base = FindAnyObjectByType<Base>();
		playerInventory = FindAnyObjectByType<PlayerInventory>();

		RegisterToListeners(true);
	}

	private void Start()
	{
		UpdateNeededResources();
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(@base != null)
			{
				@base.baseLevelledUpEvent.AddListener(OnBaseLevelledUp);
			}

			if(playerInventory != null)
			{
				playerInventory.diggableResourcePiecesChangedEvent.AddListener(OnDiggableResourcePiecesChangedEvent);
			}
		}
		else
		{
			if(@base != null)
			{
				@base.baseLevelledUpEvent.RemoveListener(OnBaseLevelledUp);
			}

			if(playerInventory != null)
			{
				playerInventory.diggableResourcePiecesChangedEvent.RemoveListener(OnDiggableResourcePiecesChangedEvent);
			}
		}
	}

	private void OnBaseLevelledUp(BaseLevel baseLevel)
	{
		UpdateNeededResources();
	}

	private void OnDiggableResourcePiecesChangedEvent(DiggableResourceType diggableResourceType, int numberOfPieces)
	{
		UpdateNeededResources();
	}

	private void UpdateNeededResources()
	{
		rockPieces = @base.GetLeftRockPieces();
		crystalPieces = @base.GetLeftCrystalPieces();

		UpdateText();
	}

	private void UpdateText()
	{
		SetText($"Rock: {rockPieces}\nCrystal: {crystalPieces}");
	}
}