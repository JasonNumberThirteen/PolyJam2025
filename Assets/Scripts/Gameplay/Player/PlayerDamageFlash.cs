using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageFlash : MonoBehaviour
{
    private SpriteRenderer[] renderers;
    private Material[] materials;
    [SerializeField] private float flashTime = 0.2f;
    private void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        PlayerStats.GotHit += Hit;

        materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
        }
    }

    private void Hit(object sender, Vector3 hitPoint)
    {
        StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f,0f, elapsedTime/flashTime);

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat("_FlashAmount", currentFlashAmount);
            }

            yield return null;
        }


        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_FlashAmount", 0);
        }

    }
}
