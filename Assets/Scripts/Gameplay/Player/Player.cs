using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInventory))]
public class Player : MonoBehaviour
{
	public UnityEvent<DiggableResource> diggableResourceUpdatedEvent;

	private PlayerInventory playerInventory;
	private InputEvents inputEvents;
	private DiggableResource diggableResource;

	public bool DiggableResourceIsDefined() => diggableResource != null;

	public void SetDiggableResource(DiggableResource diggableResource)
	{
		this.diggableResource = diggableResource;

		diggableResourceUpdatedEvent?.Invoke(diggableResource);
	}

	private void Awake()
	{
		playerInventory = GetComponent<PlayerInventory>();
		inputEvents = FindAnyObjectByType<InputEvents>();

		RegisterToCallbacks(true);
	}

	private void OnDestroy()
	{
		RegisterToCallbacks(false);
	}

	private void RegisterToCallbacks(bool register)
	{
		if(register)
		{
			if(inputEvents != null)
			{
				InputEvents.InteractAction += OnInteract;
			}
		}
		else
		{
			if(inputEvents != null)
			{
				InputEvents.InteractAction -= OnInteract;
			}
		}
	}

	private void OnInteract(object sender, EventArgs eventArgs)
    {
		if(diggableResource == null)
		{
			return;
		}

		playerInventory.AddDiggableResourcePiecesByType(diggableResource.GetDiggableResourceType(), diggableResource.GetNumberOfPieces());
		diggableResource.Dig();
    }
}