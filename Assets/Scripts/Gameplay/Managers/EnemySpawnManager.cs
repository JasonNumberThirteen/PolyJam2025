using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Timer))]
public class EnemySpawnManager : MonoBehaviour
{
	public static EventHandler EnemyDied;
	int amountOfEnemies = 4;

	private Timer timer;
	
	[SerializeField] private List<EnemyWave> enemyWaves = new();
	[SerializeField] private Transform[] spawnPoints;

	private int currentWaveIndex;
	private WaveCounterTextUI waveCounterTextUI;
	private FadeScreenImageUI fadeScreenImageUI;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		waveCounterTextUI = FindAnyObjectByType<WaveCounterTextUI>();
		fadeScreenImageUI = FindAnyObjectByType<FadeScreenImageUI>();

		EnemyDied += OnEnemyDied;

		StartWave();

		RegisterToListeners(true);
	}

    private void OnEnemyDied(object sender, EventArgs e)
    {
		amountOfEnemies--;

		if(amountOfEnemies <= 0)
		{
			StartWave();
		}
    }

    private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			//timer.timerFinishedEvent.AddListener(OnTimerFinished);
		}
		else
		{
			//timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
		}
	}

	private void OnTimerFinished()
	{
		StartWave();
	}

	private void StartWave()
	{
		if(currentWaveIndex < enemyWaves.Count)
		{
			EnemyWave currentWave = enemyWaves[currentWaveIndex];

			amountOfEnemies = currentWave.GetAmountOfEnemies();

			if(waveCounterTextUI != null)
			{
				waveCounterTextUI.DisplayWaveCounter(currentWaveIndex + 1);
			}

			++currentWaveIndex;

			StartCoroutine(SpawnEnemies(currentWave));
		}
		else
		{
			StartCoroutine(CheckIfWonGame());
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

		timer.SetDuration(3f);
		timer.StartTimer();
	}

	private IEnumerator CheckIfWonGame()
	{
		while (true)
		{
			yield return new WaitForSeconds(3);

			if(currentWaveIndex < enemyWaves.Count && FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length == 0)
			{
				FadeIn();
				StopCoroutine(CheckIfWonGame());
			}
		}
	}

	private IEnumerator CheckIfNoMoreEnemies()
	{
        while (true)
        {
            yield return new WaitForSeconds(3);

            if (FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length == 0)
            {
                FadeIn();
                StopCoroutine(CheckIfWonGame());
            }
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