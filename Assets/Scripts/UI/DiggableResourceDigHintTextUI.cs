public class DiggableResourceDigHintTextUI : TextUI
{
	private Player player;

	protected override void Awake()
	{
		base.Awake();
		
		player = FindAnyObjectByType<Player>();

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
			if(player != null)
			{
				player.diggableResourceUpdatedEvent.AddListener(OnDiggableResourceUpdated);
			}
		}
		else
		{
			if(player != null)
			{
				player.diggableResourceUpdatedEvent.RemoveListener(OnDiggableResourceUpdated);
			}
		}
	}

	private void OnDiggableResourceUpdated(DiggableResource diggableResource)
	{
		SetTextEnabled(diggableResource != null);
	}
}