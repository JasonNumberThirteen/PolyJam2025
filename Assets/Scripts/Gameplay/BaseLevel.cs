using System;
using UnityEngine;

[Serializable]
public class BaseLevel
{
	[SerializeField] private int numberOfRequiredRockResourcePieces;
	[SerializeField] private int numberOfRequiredCrystalResourcePieces;
	[SerializeField] private float playerOxygenLevel;
	[SerializeField] private int numberOfShotgunBullets;
	[SerializeField] private float shotgunReloadTime;

	public int GetNumberOfRequiredRockResourcePieces() => numberOfRequiredRockResourcePieces;
	public int GetNumberOfRequiredCrystalResourcePieces() => numberOfRequiredCrystalResourcePieces;
	public float GetPlayerOxygenLevel() => playerOxygenLevel;
	public int GetNumberOfShotgunBullets() => numberOfShotgunBullets;
	public float GetShotgunReloadTime() => shotgunReloadTime;
	public bool CanAdvanceToThisLevel(int rockResourcePieces, int crystalResourcePieces) => rockResourcePieces >= numberOfRequiredRockResourcePieces && crystalResourcePieces >= numberOfRequiredCrystalResourcePieces;
}