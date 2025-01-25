using Random = UnityEngine.Random;
using UnityEngine;

public class DiggableResource : MonoBehaviour
{
	[SerializeField] private DiggableResourceType diggableResourceType;
	
	private int lowerBoundOfNumberOfPieces = 1;
	private int upperBoundOfNumberOfPieces = 5;
	private int numberOfPieces;

	public DiggableResourceType GetDiggableResourceType() => diggableResourceType;
	public int GetNumberOfPieces() => numberOfPieces;

	public void Dig()
	{
		Destroy(gameObject);
	}

	private void Awake()
	{
		numberOfPieces = Random.Range(lowerBoundOfNumberOfPieces, upperBoundOfNumberOfPieces + 1);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.TryGetComponent(out Player player))
		{
			player.SetDiggableResource(this);
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.TryGetComponent(out Player player))
		{
			player.SetDiggableResource(null);
		}
	}
}