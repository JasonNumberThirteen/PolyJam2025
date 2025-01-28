using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
	public static EventHandler EnemyDied;
	
	[SerializeField] private List<EnemyWave> enemyWaves = new();
	[SerializeField] private Transform[] spawnPoints;

	private int currentWaveIndex;
	private int numberOfLeftEnemies;
	private WaveCounterTextUI waveCounterTextUI;
	private FadeScreenImageUI fadeScreenImageUI;

	private void Awake()
	{
		waveCounterTextUI = FindAnyObjectByType<WaveCounterTextUI>();
		fadeScreenImageUI = FindAnyObjectByType<FadeScreenImageUI>();
		EnemyDied += OnEnemyDied;

		StartWave();
	}

	private void OnDestroy()
	{
		EnemyDied -= OnEnemyDied;
	}

	private void OnEnemyDied(object sender, EventArgs eventArgs)
	{
		if(--numberOfLeftEnemies <= 0)
		{
			++currentWaveIndex;
			
			StartWave();
		}
	}

	private void StartWave()
	{
		if(currentWaveIndex < enemyWaves.Count)
		{
			var currentWave = enemyWaves[currentWaveIndex];

			numberOfLeftEnemies = currentWave.GetEnemiesGOs().Count;

			DisplayWaveCounter();
			StartCoroutine(SpawnEnemies(currentWave));
		}
		else
		{
			Invoke(nameof(FadeIn), 3f);
		}
	}

	private void DisplayWaveCounter()
	{
		if(waveCounterTextUI != null)
		{
			waveCounterTextUI.DisplayWaveCounter(currentWaveIndex + 1);
		}
	}

	private IEnumerator SpawnEnemies(EnemyWave enemyWave)
	{
		var enemiesGOs = enemyWave.GetEnemiesGOs();
		
		for (var i = 0; i < enemiesGOs.Count; ++i)
		{
			yield return new WaitForSeconds(enemyWave.GetSpawnDelay());

			Instantiate(enemiesGOs[i], spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
		}
	}

	private void FadeIn()
	{
		if(fadeScreenImageUI != null)
		{
			fadeScreenImageUI.StartFading(false);
		}
	}
}