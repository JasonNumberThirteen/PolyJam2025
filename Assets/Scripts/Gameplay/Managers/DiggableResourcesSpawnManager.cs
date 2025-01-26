using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;

public class DiggableResourcesSpawnManager : MonoBehaviour
{
	[SerializeField] private List<GameObject> diggableResourcesGOs = new();
	[SerializeField] private float instanceY = -2.7f;
	[SerializeField, Min(0f)] private float spawnDelay = 10f;
	[SerializeField, Min(0)] private int maximumNumberOfDiggableResourcesOnScene = 30;
	[SerializeField] private EdgeCollider2D[] spawnAreas;

	private void Awake()
	{
		InvokeRepeating(nameof(SpawnRandomDiggableResourceIfPossible), spawnDelay, spawnDelay);
	}

	private void SpawnRandomDiggableResourceIfPossible()
	{
		if(diggableResourcesGOs.Count == 0 || spawnAreas == null || FindObjectsByType<DiggableResource>(FindObjectsSortMode.None).Length >= maximumNumberOfDiggableResourcesOnScene)
		{
			return;
		}

		var randomSpawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
		var randomX = Random.Range(randomSpawnArea.bounds.min.x, randomSpawnArea.bounds.max.x);
		var randomPosition = new Vector2(randomX, instanceY);
		var randomIndex = Random.Range(0, diggableResourcesGOs.Count);

		Instantiate(diggableResourcesGOs[randomIndex], randomPosition, Quaternion.identity);
	}
}