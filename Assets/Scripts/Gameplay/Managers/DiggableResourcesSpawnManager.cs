using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class DiggableResourcesSpawnManager : MonoBehaviour
{
	[SerializeField] private List<GameObject> diggableResourcesGOs = new();
	[SerializeField] private float instanceY = -2.7f;
	[SerializeField, Min(0f)] private float spawnDelay = 10f;
	[SerializeField, Min(0)] private int maximumNumberOfDiggableResourcesOnScene = 30;

	private EdgeCollider2D spawnArea;

	private void Awake()
	{
		spawnArea = GetComponent<EdgeCollider2D>();
		
		InvokeRepeating(nameof(SpawnRandomDiggableResourceIfPossible), spawnDelay, spawnDelay);
	}

	private void SpawnRandomDiggableResourceIfPossible()
	{
		if(diggableResourcesGOs.Count == 0 || spawnArea == null || FindObjectsByType<DiggableResource>(FindObjectsSortMode.None).Length >= maximumNumberOfDiggableResourcesOnScene)
		{
			return;
		}

		var randomX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
		var randomPosition = new Vector2(randomX, instanceY);
		var randomIndex = Random.Range(0, diggableResourcesGOs.Count);

		Instantiate(diggableResourcesGOs[randomIndex], randomPosition, Quaternion.identity);
	}
}