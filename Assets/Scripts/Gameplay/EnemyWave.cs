using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWave
{
	[SerializeField] private List<GameObject> enemiesGOs = new();
	[SerializeField, Min(0f)] private float spawnDelay = 2f;

	public List<GameObject> GetEnemiesGOs() => enemiesGOs;
	public float GetSpawnDelay() => spawnDelay;
}